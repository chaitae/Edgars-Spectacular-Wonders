using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class LookAt : MonoBehaviour, IInteractable
{
    public CinemachineVirtualCamera cinemachineVirtualCamera;
    CharacterControls characterControls1;
    bool playerLooking = false;
    void Update()
    {
        if (playerLooking)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                playerLooking = false;
                cinemachineVirtualCamera.Priority = 1;
                characterControls1.SetMovement(true, "Lookat");
                characterControls1.canUseObject = true;
            }
        }

    }
    // public cinemachine
    public void CharacterEnter(CharacterControls characterControls)
    {
    }

    public void CharacterExit(CharacterControls characterControls)
    {
    }

    public void EquippedAction(CharacterControls characterControls)
    {
    }

    public void Interact(CharacterControls characterControls)
    {
        characterControls.canUseObject = false;
        cinemachineVirtualCamera.Priority = 100;
        characterControls.SetMovement(false, " Look at set false");
        playerLooking = true;
        characterControls1 = characterControls;
    }


}
