using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialManager : MonoBehaviour
{
    public GameEvent dialPuzzleComplete;
    public int[] dialPassword = new int[3];
    public Dial[] dials;
    int dialIndex;
    bool dialsCorrectPlacement = false;
    public LookAt lookAt;
    bool isPlayerLooking;
    public GameObject uiTips;
    void OnEnable()
    {
        lookAt.OnLookAt += ActivateListenControls;
        lookAt.OnLookAway += DeactivateListenControls;
    }
    void OnDisable()
    {
        lookAt.OnLookAt -= ActivateListenControls;
        lookAt.OnLookAway -= DeactivateListenControls;
    }
    void ActivateListenControls()
    {
        uiTips.SetActive(true);
        //UIManager._instance.ShowHintText("Press a or d to select dials");
        isPlayerLooking = true;
        dials[0].isDialSelected = true;
        dialIndex = 0;
        dials[dialIndex].isDialSelected = false;
        dialIndex--;
        if (dialIndex == -1) dialIndex = dials.Length - 1;
        dials[dialIndex].isDialSelected = true;
        dials[dialIndex].OutlineObject();
    }
    void DeactivateListenControls()
    {

        uiTips.SetActive(false);
        UIManager._instance.HideHint();
        isPlayerLooking = false;
        for (int i = 0; i < dials.Length; i++)
        {
            dials[i].isDialSelected = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
    }
    bool DialCorrectPositions()
    {
        for (int i = 0; i < dialPassword.Length; i++)
        {
            if (Mathf.Round(dials[i].dialPresets[dials[i].dialRotationIndex]) != dialPassword[i])
            {
                return false;
            }
        }
        return true;
    }
    // Update is called once per frame
    void Update()
    {
        if (isPlayerLooking)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {

                dials[dialIndex].NoOutlineObject();
                dials[dialIndex].isDialSelected = false;
                dialIndex--;
                if (dialIndex == -1) dialIndex = dials.Length - 1;
                dials[dialIndex].isDialSelected = true;
                dials[dialIndex].OutlineObject();

            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                dials[dialIndex].NoOutlineObject();
                dials[dialIndex].isDialSelected = false;
                dialIndex++;
                if (dialIndex == dials.Length) dialIndex = 0;
                dials[dialIndex].isDialSelected = true;
                dials[dialIndex].OutlineObject();

            }
            if (DialCorrectPositions() && !dialsCorrectPlacement)
            {
                Debug.Log("Wohoo it's right!");
                dialsCorrectPlacement = false;
                dialPuzzleComplete.Raise();
            }
        }
    }
}
