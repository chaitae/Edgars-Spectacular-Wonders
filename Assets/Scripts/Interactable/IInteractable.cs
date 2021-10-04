using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable 
{
    void Interact(CharacterControls characterControls);
    void CharacterEnter(CharacterControls characterControls);
    void CharacterExit(CharacterControls characterControls);
    void EquippedAction(CharacterControls characterControls);
}
