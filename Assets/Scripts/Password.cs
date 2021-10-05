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
        ui.SetActive(true);
        myinputfield.Select();
        myinputfield.ActivateInputField();
        StartCoroutine("DisableMovement");
    }
    public void HidePasswordScreen()
    {
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
