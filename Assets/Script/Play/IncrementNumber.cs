using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Services.CloudSave;

public class IncrementNumber : MonoBehaviour
{
    public TMP_Text numText;
    // Start is called before the first frame update
    void Start()
    {
        CheckData();
    }

    async void CheckData()
    {
        Dictionary<string, string> GetPlayerCondition = await CloudSaveService.Instance.Data.LoadAsync(new HashSet<string> { "PlayerCondition" });

        string PlayerCond = GetPlayerCondition["PlayerCondition"];

        if(PlayerCond.Equals("New"))
        {
            numText.text = "0";
        }
        else if(PlayerCond.Equals("Returning"))
        {
            LoadData();
        }
    }

    public async void LoadData()
    {
        Dictionary<string, string> savedNumber = await CloudSaveService.Instance.Data.LoadAsync(new HashSet<string> { "Number" });
        numText.text = savedNumber["Number"];
    }
}
