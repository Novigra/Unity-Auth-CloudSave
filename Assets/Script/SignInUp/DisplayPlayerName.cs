using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Services.CloudSave;

public class DisplayPlayerName : MonoBehaviour
{
    public TMP_Text PlayerName;
    // Start is called before the first frame update
    void Start()
    {
        LoadData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    async void LoadData()
    {
        Dictionary<string, string> PlayerNameData = await CloudSaveService.Instance.Data.LoadAsync(new HashSet<string> { "PlayerName" });
        PlayerName.text = PlayerNameData["PlayerName"];
    }
}
