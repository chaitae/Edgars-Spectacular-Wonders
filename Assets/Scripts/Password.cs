using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;
using TMPro;

public class Password : MonoBehaviour
{
    public GameObject ui;
    public TMP_InputField myinputfield;
    public GameEvent gameEvent;
    public string password;

    [YarnCommand("OpenPassword")]
    public void OpenPasswordScreen()
    {
        // UIManager._instance.ShowHintText("Press escape to leave password entry");
        ui.SetActive(true);
        myinputfield.Select();
        myinputfield.ActivateInputField();
        StartCoroutine("DisableMovement");
    }
    public void HidePasswordScreen()
    {
        // UIManager._instance.HideHint();
        ui.SetActive(false);
        CharacterControls characterControl = FindObjectOfType<CharacterControls>();
        characterControl.SetMovement(true, "ClosePassword");
    }
    IEnumerator DisableMovement()
    {
        yield return new WaitForSeconds(.5f);
        CharacterControls characterControl = FindObjectOfType<CharacterControls>();
        characterControl.SetMovement(false, "OpenPassword");

    }
    public void CompareEntryandPassword(string pass)
    {
        if (pass == password)
        {
            Debug.Log("Yes, password is right");
            HidePasswordScreen();
            gameEvent.Raise();
        }
        else
        {
            myinputfield.text = "wrong password";
        }
    }
    void Update()
    {
        if (ui.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                HidePasswordScreen();
            }
        }
    }
}
