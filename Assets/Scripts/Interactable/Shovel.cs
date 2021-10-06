using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovel : MonoBehaviour, IInteractable
{
    CharacterControls characterControls;
    bool equipped = false;
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
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(_characterControls.transform.position - transform.lossyScale.y * .75f * Vector3.up, -transform.up, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.GetComponent<IDiggable>() != null)
            {
                IDiggable diggable = hit.collider.GetComponent<IDiggable>();
                if (diggable != null) diggable.Dig();
            }
        }
        else
        {
        }
    }

    public void Interact(CharacterControls _characterControls,KeyCode keyCode)
    {
        Debug.Log("interacted shovel");
        if (_characterControls.equippedObject == null)
        {
            equipped = true;
            _characterControls.PickUp(gameObject);
            GetComponent<Rigidbody>().isKinematic = true;
            characterControls = _characterControls;

        }
    }
    void Update()
    {
        if(equipped)
        {
            // UIManager._instance.
            UIManager._instance.ShowSpecialInteraction();
        }
        if (equipped && Input.GetKeyDown(KeyCode.X))
        {
            characterControls.Drop();
            characterControls = null;
            equipped = false;
        }
    }

}
