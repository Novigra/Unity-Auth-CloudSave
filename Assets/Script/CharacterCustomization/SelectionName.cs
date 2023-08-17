using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CharacterSelection;
using TMPro;
using Unity.VisualScripting;

public class SelectionName : MonoBehaviour
{
    [SerializeField] TMP_Text SelectionText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ModifyHeadline(ESelectionPhase selection)
    {
        if (selection == ESelectionPhase.PrimaryWeaponSelection)
        {
            SelectionText.text = "Primary Weapon";
        }
    }
}
