using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChangeButton : MonoBehaviour
{
    [SerializeField] Object CharacterSelectionObject;

    CharacterSelection CharSelec;

    Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnPress);

        CharSelec = CharacterSelectionObject.GetComponent<CharacterSelection>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnPress()
    {
       CharSelec.ChangeSelection();
    }
}
