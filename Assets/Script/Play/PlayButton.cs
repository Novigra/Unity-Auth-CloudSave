using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    [SerializeField] UnityEngine.Object numberObject;

    private IncrementNumber num;

    private Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnPress);

        num = numberObject.GetComponent<IncrementNumber>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnPress()
    {
        int currentNumber = Int16.Parse(num.numText.text);
        currentNumber++;
        num.numText.text = currentNumber.ToString();
    }
}
