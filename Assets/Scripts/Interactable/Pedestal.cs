using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestal : MonoBehaviour, IInteractable
{
    public GameObject objectPosition;
    public GameObject placedObject;
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
        if (characterControls.equippedObject != null)
        {
            characterControls.equippedObject.transform.position = objectPosition.transform.position;
            characterControls.equippedObject.transform.parent = null;
            characterControls.equippedObject.GetComponent<Rigidbody>().isKinematic = false;
            placedObject = characterControls.equippedObject;
            characterControls.equippedObject = null;
        }
        else
        {
            characterControls.PickUp(placedObject);
            placedObject = null;
        }
    }

}
