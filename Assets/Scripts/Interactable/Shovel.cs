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
        UIManager._instance.ChangeInteractionText("Press E to pick up");

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
                Debug.Log("hit floor");
                IDiggable diggable = hit.collider.GetComponent<IDiggable>();
                if (diggable != null) diggable.Dig();
                Debug.Log("equipped aciton happened");
            }
        }
        else
        {
        }
    }
    void CheckDiggables()
    {
        RaycastHit hit;
        if (Physics.Raycast(characterControls.transform.position - transform.lossyScale.y * .75f * Vector3.up, -transform.up, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.GetComponent<IDiggable>() != null)
            {
                UIManager._instance.ShowSpecialInteraction();
                UIManager._instance.ChangeSpecialInteractionText("Press E to dig");
            }

            else
            {
                // UIManager._instance.HideSpecialInteraction();
                UIManager._instance.ChangeSpecialInteractionText("Press X to drop shovel");
            }
        }
    }

    public void Interact(CharacterControls _characterControls, KeyCode keyCode)
    {
        Debug.Log("interacted shovel");
        if (_characterControls.equippedObject == null)
        {

            UIManager._instance.ChangeSpecialInteractionText("Press X to drop shovel");
            UIManager._instance.ShowSpecialInteraction();
            equipped = true;
            _characterControls.PickUp(gameObject);
            characterControls = _characterControls;

        }
    }
    void Update()
    {
        if (equipped)
        {
            CheckDiggables();
        }
        if (equipped && Input.GetKeyDown(KeyCode.X))
        {

            UIManager._instance.HideSpecialInteraction();
            characterControls.Drop();
            characterControls = null;
            equipped = false;
        }
    }

}
