using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class LookAt : MonoBehaviour, IInteractable
{
    public CinemachineVirtualCamera cinemachineVirtualCamera;
    CharacterControls characterControls;
    public bool hidePlayerOnView = false;
    bool playerLooking = false;
    void Update()
    {
        if (playerLooking)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.X))
            {
                playerLooking = false;
                cinemachineVirtualCamera.Priority = 1;
                characterControls.SetMovement(true, "Lookat");
                characterControls.canUseObject = true;
                UIManager._instance.ContinueCheckingForNearInteractable();
                Camera.main.cullingMask = ~0;

            }
        }

    }
    // public cinemachine
    public void CharacterEnter(CharacterControls _characterControls)
    {
        UIManager._instance.ShowInteractionText();
    }

    public void CharacterExit(CharacterControls _characterControls)
    {

        UIManager._instance.HideInteractionText();
    }

    public void EquippedAction(CharacterControls _characterControls)
    {
    }

    public void Interact(CharacterControls _characterControls, KeyCode keyCode)
    {
        Debug.Log("look at button");
        _characterControls.canUseObject = false;
        cinemachineVirtualCamera.Priority = 100;
        _characterControls.SetMovement(false, " Look at set false");
        playerLooking = true;
        characterControls = _characterControls;
        UIManager._instance.StopCheckingForNearInteractable();
        UIManager._instance.HideInteractionText();
        if (hidePlayerOnView)
        {
            Camera.main.cullingMask = ~LayerMask.GetMask("Player");
        }

    }


}
