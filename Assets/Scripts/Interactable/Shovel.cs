using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovel : MonoBehaviour, IInteractable
{
    CharacterControls characterControls1;
    bool equipped = false;
    public void CharacterEnter(CharacterControls characterControls)
    {
    }

    public void CharacterExit(CharacterControls characterControls)
    {
    }

    public void EquippedAction(CharacterControls characterControls)
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(characterControls.transform.position - transform.lossyScale.y * .75f * Vector3.up, -transform.up, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.name == "grave")
            {
                IDiggable diggable = hit.collider.GetComponent<IDiggable>();
                if(diggable != null) diggable.Dig();
            }
        }
        else
        {
        }
    }

    public void Interact(CharacterControls characterControls)
    {
        Debug.Log("interacted shovel");
        if (characterControls.equippedObject == null)
        {
            equipped = true;
            characterControls.PickUp(gameObject);
            GetComponent<Rigidbody>().isKinematic = true;
            characterControls1 = characterControls;

        }
    }
    void Update()
    {
        if(equipped && Input.GetKeyDown(KeyCode.X))
        {
            characterControls1.Drop();
            characterControls1 = null;
        }
    }

}
