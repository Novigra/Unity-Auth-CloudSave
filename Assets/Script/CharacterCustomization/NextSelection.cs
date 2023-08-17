using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static CharacterSelection;

public class NextSelection : MonoBehaviour
{
    [SerializeField] Object CharacterSelectionObject;
    [SerializeField] Object SelectionNameObject;
    [SerializeField] TMP_Text ButtonText;

    private CharacterSelection CharSelec;
    private SelectionName SelecName;

    private Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnPress);

        CharSelec = CharacterSelectionObject.GetComponent<CharacterSelection>();
        SelecName = SelectionNameObject.GetComponent<SelectionName>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnPress()
    {
        if(CharSelec.selectionPhase == ESelectionPhase.CharacterSelection)
        {
            CharSelec.SwitchProperties();
            SelecName.ModifyHeadline(CharSelec.selectionPhase);
            
            CharSelec.LoadSelection();

            ButtonText.text = "Ready";
        }
        else
        {
            CharSelec.SaveSelection();
        }
    }
}
