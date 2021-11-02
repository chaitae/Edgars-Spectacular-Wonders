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
    public GameObject obelisk;
    public string password;
    CharacterControls characterControl;
    void Start()
    {
        characterControl = FindObjectOfType<CharacterControls>();
    }

    [YarnCommand("OpenPassword")]
    public void OpenPasswordScreen()
    {
        ui.SetActive(true);
        myinputfield.Select();
        myinputfield.ActivateInputField();
    }
    public void HidePasswordScreen()
    {
        UIManager._instance.HideHint();
        ui.SetActive(false);

    }
    IEnumerator SetMovement(bool moveState)
    {
        StopAllCoroutines();
        yield return new WaitForSeconds(.5f);
        CharacterControls characterControl = FindObjectOfType<CharacterControls>();
        characterControl.SetMovement(moveState, "OpenPassword");

    }
    public void CompareEntryandPassword(string pass)
    {
        if (pass.ToLower() == password.ToLower())
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
            characterControl.SetMovement(false, "password");
            UIManager._instance.disableSettingsMenu = true;
            if (Input.GetKeyDown(KeyCode.Escape))
            {

                characterControl.SetMovement(true, "password");
                UIManager._instance.disableSettingsMenu = false;
                HidePasswordScreen();
            }
        }
        else
        {

        }
    }
}
