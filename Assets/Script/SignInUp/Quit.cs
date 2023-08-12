using System.Collections;
using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Quit : MonoBehaviour
{
    [SerializeField] Object IncNumObject;

    private IncrementNumber IncNum;

    public Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnPress);

        IncNum = IncNumObject.GetComponent<IncrementNumber>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    async void OnPress()
    {
        var SaveNumber = new Dictionary<string, object> { { "Number", IncNum.numText.text } };
        await CloudSaveService.Instance.Data.ForceSaveAsync(SaveNumber);

        AuthenticationService.Instance.SignOut();

        SceneManager.LoadScene(sceneName: "SignInScene");
    }
}
