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
        if (Physics.Raycast(characterControls.transform.position + characterControls.transform.forward * 2 + Vector3.up, -Vector3.up, out hit, Mathf.Infinity))
        {
            IDiggable diggable = hit.collider.gameObject.GetComponent<IDiggable>();
            if (diggable != null)
            {
                Debug.Log("hit floor");
                if (diggable != null) diggable.Dig();
                Debug.Log("equipped aciton happened");
                if(diggable.HasBeenDug())
                UIManager._instance.HideSpecialInteraction();
            }
        }
    }
    bool CheckDiggables()
    {
        RaycastHit hit;
        if (Physics.Raycast(characterControls.transform.position + characterControls.transform.forward * 2 + Vector3.up, -Vector3.up, out hit, Mathf.Infinity))
        {
            IDiggable temp = hit.collider.gameObject.GetComponent<IDiggable>();
            if (temp != null)
            {
                if (!temp.HasBeenDug())
                {
                    UIManager._instance.ShowSpecialInteraction();
                    UIManager._instance.ChangeSpecialInteractionText("Press E to dig");
                }
                return true;
            }
            else
            {
                UIManager._instance.ChangeSpecialInteractionText("Press X to drop shovel");
            }
        }

        return false;
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
            if (characterControls.CanDrop())
            {
                characterControls.Drop();
                characterControls = null;
                equipped = false;
            }
        }
    }

}
