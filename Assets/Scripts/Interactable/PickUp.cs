using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour, IInteractable
{
    public void CharacterEnter(CharacterControls characterControls)
    {
        UIManager._instance.ShowInteractionText();
    }

    public void CharacterExit(CharacterControls characterControls)
    {
        UIManager._instance.HideInteractionText();

    }

    public void Interact(CharacterControls characterControls,KeyCode keyCode)
    {
        if (characterControls.equippedObject == null)
        {

            characterControls.PickUp(gameObject);
            if (GetComponent<Rigidbody>() != null)
            {
                GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }
    public void EquippedAction(CharacterControls characterControls)
    {
        characterControls.Drop();
    }
}
