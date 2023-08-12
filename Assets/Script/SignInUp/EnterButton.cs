using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.CloudSave;

public class EnterButton : MonoBehaviour
{
    [SerializeField] Object userObject;
    [SerializeField] Object PassObject;

    private UsernameSetup user;
    private PasswordSetup pass;

    public Button button;

    private Scene scene;

    private bool IsValid;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Press);

        user = userObject.GetComponent<UsernameSetup>();
        pass = PassObject.GetComponent<PasswordSetup>();

        IsValid = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public async void Press()
    {
        scene = SceneManager.GetActiveScene();

        if (scene.name.Equals("SignInScene"))
        {
            await SignInAsync(user.username.text, pass.password.text);

            if(IsValid)
            {
                var PlayerCondition = new Dictionary<string, object> { { "PlayerCondition", "Returning" } };
                await CloudSaveService.Instance.Data.ForceSaveAsync(PlayerCondition);

                SceneManager.LoadScene(sceneName: "PlayScene");
            }
        }
        else
        {
            Regex regexUsername = new Regex("^[A-Za-z0-9.,_@-]{3,20}$");
            Match matchUsername = regexUsername.Match(user.username.text);

            Regex regexPassword = new Regex("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[!@#$%^&*()_+\\-=\\[\\]{};':\"\\\\|,.<>\\/?]).{8,30}$");
            Match matchPassword = regexPassword.Match(pass.password.text);

            if (matchUsername.Success && matchPassword.Success)
            {
                await SignUpAsync(user.username.text, pass.password.text);

                var PlayerCondition = new Dictionary<string, object> { { "PlayerCondition", "New" } };
                await CloudSaveService.Instance.Data.ForceSaveAsync(PlayerCondition);

                Debug.Log("nicely done!");
                SceneManager.LoadScene(sceneName: "PlayerNameScene");
            }
        }

        
    }

    async Task SignInAsync(string username, string password)
    {
        try
        {
            await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(username, password);
            Debug.Log("Sign in successful");
            IsValid = true;
        }
        catch(AuthenticationException e)
        {
            Debug.LogException(e);
        }
        catch(RequestFailedException e)
        {
            Debug.LogException(e);
        }
    }

    async Task SignUpAsync(string username, string password)
    {
        try
        {
            await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(username, password);
            Debug.Log("Sign up successful");
        }
        catch(AuthenticationException e)
        {
            Debug.LogException(e);
        }
        catch(RequestFailedException e)
        {
            Debug.LogException(e);
        }
    }
}
