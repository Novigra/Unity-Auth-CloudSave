using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResetPasswordButton : MonoBehaviour
{
    [SerializeField] Object CPObject;
    [SerializeField] Object NPObject;

    private CurrentPassword CP;
    private NewPassword NP;

    public Button button;

    bool IsValid;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnPress);
        
        CP = CPObject.GetComponent<CurrentPassword>();
        NP = NPObject.GetComponent<NewPassword>();

        IsValid = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public async void OnPress()
    {
        await UpdatePasswordAsync(CP.CurrentPass.text, NP.NewPass.text);

        if(IsValid)
        {
            SceneManager.LoadScene(sceneName: "SignInScene");
        }
    }

    async Task UpdatePasswordAsync(string currentPassword, string newPassword)
    {
        try
        {
            await AuthenticationService.Instance.UpdatePasswordAsync(currentPassword, newPassword);
            Debug.Log("Password updated");
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
}
