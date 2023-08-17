using System.Collections;
using System.Collections.Generic;
using Unity.Services.CloudSave;
using UnityEngine;
//using UnityEngine.Windows;
using static CharacterSelection;

public class PlayerCharacter : MonoBehaviour
{
    public struct EquippedPlayerCharacter
    {
        public string EquippedCharacter;
        public string EquippedWeapon;
    }

    private EquippedPlayerCharacter equippedPlayerCharacter;
    public GameObject Character;
    public GameObject Weapon;

    Vector3 SpawnPosition;
    Quaternion SpawnRotation;
    // Start is called before the first frame update
    void Start()
    {
        LoadCharacter();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log(equippedPlayerCharacter.EquippedCharacter);
            Debug.Log(equippedPlayerCharacter.EquippedWeapon);
        }
    }

    async void LoadCharacter()
    {
        Dictionary<string, string> GetCharacter = await CloudSaveService.Instance.Data.LoadAsync(new HashSet<string> { "Character" });
        equippedPlayerCharacter.EquippedCharacter = GetCharacter["Character"];

        Dictionary<string, string> GetWeapon = await CloudSaveService.Instance.Data.LoadAsync(new HashSet<string> { "Weapon" });
        equippedPlayerCharacter.EquippedWeapon = GetWeapon["Weapon"];

        SpawnCharacter();
    }

    void SpawnCharacter()
    {
        // Character
        Character = Instantiate(Resources.Load<GameObject>($"Models/Character/{equippedPlayerCharacter.EquippedCharacter}"));

        SpawnPosition = GameObject.Find("CharacterLocation").transform.position;
        SpawnRotation = GameObject.Find("CharacterLocation").transform.rotation;

        Character.transform.position = SpawnPosition;
        Character.transform.rotation = SpawnRotation;

        // Weapon
        Weapon = Instantiate(Resources.Load<GameObject>($"Models/Weapon/{equippedPlayerCharacter.EquippedWeapon}"));

        SpawnPosition = GameObject.Find("WeaponLocation").transform.position;
        SpawnRotation = GameObject.Find("WeaponLocation").transform.rotation;

        Weapon.transform.position = SpawnPosition;
        Weapon.transform.rotation = SpawnRotation;
    }
}
