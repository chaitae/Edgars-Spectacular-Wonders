using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour, IInteractable
{
    public void CharacterEnter(CharacterControls characterControls)
    {
    }

    public void CharacterExit(CharacterControls characterControls)
    {
    }

    public void Interact(CharacterControls characterControls)
    {
        if (characterControls.equippedObject == null)
        {

            characterControls.PickUp(gameObject);
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    public void EquippedAction(CharacterControls characterControls)
    {
        characterControls.Drop();
    }
}
