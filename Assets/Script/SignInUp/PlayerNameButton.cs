using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using Unity.Services.Core;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerNameButton : MonoBehaviour
{
    [SerializeField] Object PN_Object;

    PlayerName Player;

    Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnPress);

        Player = PN_Object.GetComponent<PlayerName>();
    }

    async void OnPress()
    {
        Regex regexName = new Regex("^\\S{1,50}$");
        Match matchName = regexName.Match(Player.PName.text);

        if(matchName.Success)
        {
            var data = new Dictionary<string, object> { { "PlayerName", Player.PName.text } };
            await CloudSaveService.Instance.Data.ForceSaveAsync(data);

            SceneManager.LoadScene(sceneName: "PlayScene");
        }
        
    }
}
