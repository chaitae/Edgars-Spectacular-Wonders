using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grave : MonoBehaviour,IDiggable
{
    public GameObject treasure;
    public GameObject treasureLandingPosition;
    public void Dig()
    {
        if(treasure)
        {

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
