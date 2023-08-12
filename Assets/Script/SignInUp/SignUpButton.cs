using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SignUpButton : MonoBehaviour
{
    private Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnPress);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnPress()
    {
        SceneManager.LoadScene(sceneName: "SignUpScene");
    }
}
