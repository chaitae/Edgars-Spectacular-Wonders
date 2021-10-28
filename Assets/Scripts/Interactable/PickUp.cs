using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour, IInteractable
{
    CharacterControls characterControls1;
    public Items.Ingredient ingredientName;
    public string itemName;
    public bool infiniteObject = false;
    public void CharacterEnter(CharacterControls characterControls)
    {
        characterControls1 = characterControls;
        UIManager._instance.ShowInteractionText();
        UIManager._instance.ChangeInteractionText("E to pick up item");
    }

    public void CharacterExit(CharacterControls characterControls)
    {
        UIManager._instance.HideInteractionText();

    }
    public void Consume()
    {
//        characterControls1.ConsumeItem();
        Destroy(transform.parent.gameObject);
    }
    public void Interact(CharacterControls characterControls, KeyCode keyCode)
    {
        if (characterControls.equippedObject == null)
        {
            if (infiniteObject)
            {
                GameObject temp = Instantiate(gameObject);
                temp.GetComponent<PickUp>().infiniteObject = false;
                characterControls1 = characterControls;
                characterControls.PickUp(temp);

            }
            else
            {
                characterControls1 = characterControls;
                characterControls.PickUp(gameObject);
            }

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
