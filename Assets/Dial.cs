using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dial : MonoBehaviour
{
    public GameObject dial;
    public int[] dialPresets = { 0, -90, -180, -270 };
    public int dialRotationIndex = 0;
    public bool isDialSelected = false;

    public MeshRenderer[] meshRenderers;

    [ContextMenu("OutlineObject")]
    public void OutlineObject()
    {
        meshRenderers[0].material.shader = Shader.Find("Outlined/Custom");
        
        meshRenderers[1].material.shader = Shader.Find("Outlined/Custom");
    }
    [ContextMenu("DeoutlineObject")]
    public void NoOutlineObject()
    {
        meshRenderers[0].material.shader = Shader.Find("Standard");
        
        meshRenderers[1].material.shader = Shader.Find("Standard");

    }
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
            if (Input.GetKeyDown(KeyCode.F))
            {
                TurnDial();
            }
        }
    }
}
