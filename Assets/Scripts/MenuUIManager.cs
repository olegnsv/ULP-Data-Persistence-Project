using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
    using UnityEditor;
#endif

public class MenuUIManager : MonoBehaviour
{

    public TMP_InputField nameInputField;
    public string userName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        userName = nameInputField.text;
    }

    public void PlayButtonCall()
    {
        if (userName != "") // I hope there is better way of doing this "null".
        {
           SceneManager.LoadScene(1); 
        }
        
    }

    public void QuitButtonCall()
    {
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #else
        Application.Quit();
        #endif
    }
}
