using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CheckCache();
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            GetPlayerName();
        }
    }

    void CheckCache()
    {
        if (!AuthenticationService.Instance.SessionTokenExists)
        {
            return;
        }

        Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}");

        Debug.Log($"Access Token: {AuthenticationService.Instance.AccessToken}");
    }

    async void GetPlayerName()
    {
        try
        {
            await AuthenticationService.Instance.GetPlayerNameAsync();
        }
        catch (AuthenticationException e)
        {
            Debug.LogException(e);
        }
        catch (RequestFailedException e)
        {
            Debug.LogException(e);
        }
    }
}
