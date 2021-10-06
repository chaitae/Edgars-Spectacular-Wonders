﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Can place whatever item on this thing puts it specific location
public class Pedestal : MonoBehaviour, IInteractable
{
    public GameObject objectPosition;
    public GameObject placedObject;
    public void CharacterEnter(CharacterControls characterControls)
    {
        if (characterControls.equippedObject != null)
            UIManager._instance.ShowInteractionText();

        if (placedObject != null)
        {
            if (placedObject.GetComponent<SpecialNPC>() != null)
            {
                placedObject.GetComponent<SpecialNPC>().ShowSpecial();
                UIManager._instance.ShowInteractionText();
            }
        }
    }

    public void CharacterExit(CharacterControls characterControls)
    {
        UIManager._instance.HideInteractionText();

        if (placedObject != null)
        {
            if (placedObject.GetComponent<SpecialNPC>() != null)
            {
                placedObject.GetComponent<SpecialNPC>().HideSpecial();

            }
        }
    }

    public void EquippedAction(CharacterControls characterControls)
    {
    }

    public void Interact(CharacterControls characterControls, KeyCode keyCode)
    {
        if (keyCode == KeyCode.T)
        {
            if (placedObject.GetComponent<SpecialNPC>() != null)
            {
                placedObject.GetComponent<SpecialNPC>().Interact(characterControls, keyCode);
            }
        }

        if (characterControls.equippedObject != null)
        {
            characterControls.equippedObject.transform.position = objectPosition.transform.position;
            characterControls.equippedObject.transform.parent = null;

            if (characterControls.equippedObject.GetComponent<Rigidbody>() != null)
                characterControls.equippedObject.GetComponent<Rigidbody>().isKinematic = false;
            placedObject = characterControls.equippedObject;
            characterControls.equippedObject = null;
            if (placedObject.GetComponent<SpecialNPC>() != null)
            {
                placedObject.GetComponent<SpecialNPC>().ShowSpecial();
            }
        }
        else if (keyCode != KeyCode.T)
        {
            if (placedObject != null)
                characterControls.PickUp(placedObject);
            placedObject = null;
        }
    }

}
