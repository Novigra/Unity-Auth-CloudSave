using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using System;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;

public class InitializeSession : MonoBehaviour
{
    private List<string> keys;
    async void Awake()
    {
        try
        {
            await UnityServices.InitializeAsync();
            keys = await CloudSaveService.Instance.Data.RetrieveAllKeysAsync();
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        Debug.Log("Connected!");
    }
}
