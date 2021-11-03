using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenuUI : MonoBehaviour
{
    public string enteredName;
    [SerializeField] GameObject errorText;
    [SerializeField] GameObject inputField;
    [SerializeField] Text inputFieldText;

    public void StartNew()
    {
        if (CheckPlayerName())
        { 
            SceneManager.LoadScene(1); 
        }
        else
        {
            Debug.LogError("Player name needed");
            return;
        }
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }

    public bool CheckPlayerName()
    {
        bool hasName = false;

        if (enteredName == "")
        {
            errorText.SetActive(true);
        }
        else
        {
            inputField.SetActive(false);
            hasName = true;
            //Debug.Log("Player " + enteredName);
        }


        return hasName;
    }

    public void SetName()
    {
        enteredName = inputFieldText.text;
        DataTransporter.transporterInstance.SetNameForThisSession(enteredName);
    }
}
