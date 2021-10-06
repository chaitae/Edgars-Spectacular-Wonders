﻿/*

The MIT License (MIT)

Copyright (c) 2015-2017 Secret Lab Pty. Ltd. and Yarn Spinner contributors.

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

*/

using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace Yarn.Unity.Example
{
    /// attached to the non-player characters, and stores the name of the Yarn
    /// node that should be run when you talk to them.
    // public class 
    [System.Serializable]
    public class GameEventDialogueNode
    {
        public GameEvent gameEvent;
        public string dialogueNode;
    }
    public class NPC : MonoBehaviour, IInteractable
    {
        
        public List<GameEventDialogueNode> gameEventDialogueNodes = new List<GameEventDialogueNode>();
        DialogueUI dialogueUI;
        public List<string> ItemTalkNodes;
        public string characterName = "";
        public string defaultTalkNode = "";
        bool listening = false;
        bool characterNear = false;
        CharacterControls characterControls1;

        [Header("Optional")]
        public YarnProgram scriptToLoad;
    
        public void CharacterEnter(CharacterControls characterControls)
        {
            characterControls1 = characterControls;
            characterControls.canUseObject = false;

        }

        public void CharacterExit(CharacterControls characterControls)
        {
            characterControls1 = null;
            characterControls.canUseObject = true;
        }

        public void EquippedAction(CharacterControls characterControls)
        {
        }
        string CheckPlayerShowingKeyDialogueItem(string equippedItemName)
        {
            foreach (string talkNode in ItemTalkNodes)
            {
                //Debug.Log(talkNode + "talk nodes!");
                if (equippedItemName.ToLower().Equals(talkNode.ToLower()))
                {
                    // talkNodetoUse = talkNode;
                    // Debug.Log(talkNode);
                    return talkNode;
                }
            }
            return "";
        }
        string CheckEventsTriggered()
        {
            if (gameEventDialogueNodes.Count == 0) return "";
            for (int i = gameEventDialogueNodes.Count - 1; i >= 0; i--)
            {
                if (gameEventDialogueNodes[i].gameEvent.eventRaised)
                {
                    return gameEventDialogueNodes[i].dialogueNode;
                }
            }
            return "";
        }
        public void Interact(CharacterControls characterControls, KeyCode keyCode)
        {
            DialogueRunner dialogueRunner = FindObjectOfType<DialogueRunner>();
            if (dialogueRunner.IsDialogueRunning) return;

            string talkNodetoUse = defaultTalkNode;
            if (CheckEventsTriggered() != "") talkNodetoUse = CheckEventsTriggered();
            if (characterControls.equippedObject != null)
            {
                if (CheckPlayerShowingKeyDialogueItem(characterControls.equippedObject.name) != "")
                {
                    talkNodetoUse = CheckPlayerShowingKeyDialogueItem(characterControls.equippedObject.name);
                }
            }

            dialogueRunner.StartDialogue(talkNodetoUse);


            characterControls.SetMovement(false, "NPC");
            dialogueRunner = FindObjectOfType<DialogueRunner>();
            listening = true;
            dialogueRunner.onDialogueComplete.AddListener(() => EnableMovement(characterControls));
            dialogueRunner.onDialogueComplete.AddListener(() => UIManager._instance.ContinueCheckingForNearInteractable());
            UIManager._instance.StopCheckingForNearInteractable();
            UIManager._instance.HideInteractionText();
        }
        void EnableMovement(CharacterControls characterControls)
        {
            characterControls.SetMovement(true, "NPC");
        }

        void Start()
        {
            if (scriptToLoad != null)
            {
                DialogueRunner dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
                dialogueRunner.Add(scriptToLoad);
            }
            dialogueUI = FindObjectOfType<Yarn.Unity.DialogueUI>();
        }
        void Update()
        {
            if (listening)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    dialogueUI.MarkLineComplete();
                }
            }
        }
    }

}
