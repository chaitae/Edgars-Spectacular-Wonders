using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    public static UIManager _instance;
    public TMP_Text interactionText;
    public GameObject interactionObject;

    public GameObject interactionTalkObject;
    string interactionTestDefault = "Press E to interact";
    public void Awake()
    {
        if (_instance == null) _instance = this;
        else
        {
            Destroy(gameObject);
        }
    }
    public void ChangeInteractionText(string str)
    {
        if(str == "") interactionText.text = interactionTestDefault;
        else interactionText.text = str;
    }
    public void ShowSpecialInteraction()
    {
        interactionTalkObject.SetActive(true);
    }
    public void HideSpecialInteraction()
    {
        interactionTalkObject.SetActive(false);
    }
    public void ShowInteractionText()
    {
        interactionObject.SetActive(true);
    }
    public void HideInteractionText()
    {
        interactionObject.SetActive(false);
    }
    public void StopCheckingForNearInteractable()
    {
        // CharacterControls.OnNearInteractable -= ShowInteractionText;
    }
    public void ContinueCheckingForNearInteractable()
    {
        // CharacterControls.OnNearInteractable += ShowInteractionText;
    }
    void OnEnable()
    {
        // CharacterControls.OnNearInteractable += ShowInteractionText;
        // CharacterControls.OnLeaveInteractable += HideInteractionText;
    }
    void OnDisable()
    {
        // CharacterControls.OnNearInteractable -= ShowInteractionText;
        // CharacterControls.OnLeaveInteractable -= HideInteractionText;

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
