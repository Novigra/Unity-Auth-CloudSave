using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Services.CloudSave;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class CharacterSelection : MonoBehaviour
{
    public struct CharacterProperties
    {
        public string Character;
        public string Weapon;
    }

    public enum ESelectionPhase
    {
        CharacterSelection,
        PrimaryWeaponSelection
    }

    public enum ECharacterSelection
    {
        Spartan01,
        Spartan02
    }

    public enum EPrimaryWeaponSelection
    {
        DMR,
        SMG
    }

    public ESelectionPhase selectionPhase;

    public ECharacterSelection characterSelection;
    public EPrimaryWeaponSelection primaryWeaponSelection;

    public GameObject currentObject;

    Vector3 SpawnPosition;
    Quaternion SpawnRotation;

    public CharacterProperties characterProperties;

    // Start is called before the first frame update
    void Start()
    {
        selectionPhase = ESelectionPhase.CharacterSelection;

        LoadSelection();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(characterSelection.ToString());
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log(primaryWeaponSelection.ToString());
        }
    }

    public void LoadSelection()
    {
        if(selectionPhase == ESelectionPhase.CharacterSelection)
        {
            characterSelection = ECharacterSelection.Spartan01;

            currentObject = Instantiate(Resources.Load<GameObject>("Models/Character/Spartan01"));

            SpawnPosition = GameObject.Find("CharacterLocation").transform.position;
            SpawnRotation = GameObject.Find("CharacterLocation").transform.rotation;

            currentObject.transform.position = SpawnPosition;
            currentObject.transform.rotation = SpawnRotation;
        }
        else if(selectionPhase == ESelectionPhase.PrimaryWeaponSelection)
        {
            Destroy(currentObject);

            primaryWeaponSelection = EPrimaryWeaponSelection.DMR;

            currentObject = Instantiate(Resources.Load<GameObject>("Models/Weapon/DMR"));

            SpawnPosition = GameObject.Find("WeaponLocation").transform.position;
            SpawnRotation = GameObject.Find("WeaponLocation").transform.rotation;

            currentObject.transform.position = SpawnPosition;
            currentObject.transform.rotation = SpawnRotation;
        }
    }

    public void ChangeSelection()
    {
        if(selectionPhase == ESelectionPhase.CharacterSelection)
        {
            if (characterSelection == ECharacterSelection.Spartan01)
            {
                Destroy(currentObject);

                currentObject = Instantiate(Resources.Load<GameObject>("Models/Character/Spartan02"));
                currentObject.transform.position = SpawnPosition;
                currentObject.transform.rotation = SpawnRotation;

                characterSelection = ECharacterSelection.Spartan02;
            }
            else
            {
                Destroy(currentObject);

                currentObject = Instantiate(Resources.Load<GameObject>("Models/Character/Spartan01"));
                currentObject.transform.position = SpawnPosition;
                currentObject.transform.rotation = SpawnRotation;

                characterSelection = ECharacterSelection.Spartan01;
            }
        }
        else if(selectionPhase == ESelectionPhase.PrimaryWeaponSelection)
        {
            if(primaryWeaponSelection == EPrimaryWeaponSelection.DMR)
            {
                Destroy(currentObject);

                currentObject = Instantiate(Resources.Load<GameObject>("Models/Weapon/SMG"));
                currentObject.transform.position = SpawnPosition;
                currentObject.transform.rotation = SpawnRotation;

                primaryWeaponSelection = EPrimaryWeaponSelection.SMG;
            }
            else
            {
                Destroy(currentObject);

                currentObject = Instantiate(Resources.Load<GameObject>("Models/Weapon/DMR"));
                currentObject.transform.position = SpawnPosition;
                currentObject.transform.rotation = SpawnRotation;

                primaryWeaponSelection = EPrimaryWeaponSelection.DMR;
            }
        }
    }

    public void SwitchProperties()
    {
        if(selectionPhase == ESelectionPhase.CharacterSelection)
        {
            selectionPhase = ESelectionPhase.PrimaryWeaponSelection;
        }
    }

    public async void SaveSelection()
    {
        characterProperties.Character = characterSelection.ToString();
        characterProperties.Weapon = primaryWeaponSelection.ToString();

        var SaveCharacter = new Dictionary<string, object> { { "Character", characterProperties.Character } };
        await CloudSaveService.Instance.Data.ForceSaveAsync(SaveCharacter);

        var SaveWeapon = new Dictionary<string, object> { {"Weapon", characterProperties.Weapon } };
        await CloudSaveService.Instance.Data.ForceSaveAsync(SaveWeapon);

        SceneManager.LoadScene(sceneName: "ShowCharacterScene");
    }
}
