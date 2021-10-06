using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dial : MonoBehaviour
{
    public GameObject dial;
    public int[] dialPresets = { 0, -90, -180, -270 };
    public int dialRotationIndex = 0;
    public bool isDialSelected = false;
    public void SelectedDial()
    {
        isDialSelected = true;
    }
    void TurnDial()
    {
        dialRotationIndex++;
        if (dialRotationIndex == dialPresets.Length)
        {
            dialRotationIndex = 0;
        }
        dial.transform.eulerAngles = new Vector3(dial.transform.rotation.x, dial.transform.rotation.y, dialPresets[dialRotationIndex]);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isDialSelected)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                TurnDial();
            }
        }
    }
}
