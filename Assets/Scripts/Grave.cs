using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grave : MonoBehaviour,IDiggable
{
    public GameObject treasure;
    public GameObject treasureLandingPosition;
    public GameObject dugGraveMesh;
    public GameObject graveMesh;
    public void Dig()
    {
        Debug.Log("I'm digging");
        if(graveMesh != null)
        graveMesh.SetActive(false);
        if(dugGraveMesh != null)
        dugGraveMesh.SetActive(true);
        if(treasure != null )
        {
            treasure.SetActive(true);
        }
        // Debug.Log("You dug a grave!");
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
