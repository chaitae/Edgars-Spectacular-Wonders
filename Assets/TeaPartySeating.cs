using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeaPartySeating : MonoBehaviour
{
    public GameEvent gameEvent;
    public Pedestal[] pedestals;
    public bool isSeatedWell = false;

    [ContextMenu("Set ghosts")]
    public void SetGhostSeating()
    {
        pedestals[0].placedObject = GameObject.Find("E");
        pedestals[1].placedObject = GameObject.Find("B");
        pedestals[2].placedObject = GameObject.Find("C");
        pedestals[3].placedObject = GameObject.Find("G");
        pedestals[4].placedObject = GameObject.Find("A");
        pedestals[5].placedObject = GameObject.Find("F");
        pedestals[6].placedObject = GameObject.Find("D");
        pedestals[7].placedObject = GameObject.Find("H");
    }
    bool isNearPerson(int index, string personName)
    {
        if (index == 0)
        {
            if (pedestals[7].placedObject.name.ToLower() == personName)
            {
                return true;
            }
            if (pedestals[1].placedObject.name.ToLower() == personName)
            {
                return true;
            }
        }
        else if (index == 7)
        {
            if (pedestals[6].placedObject.name.ToLower() == personName) return true;
            if (pedestals[0].placedObject.name.ToLower() == personName) return true;

        }
        else
        {
            if (pedestals[index - 1].placedObject.name.ToLower() == personName)
            {
                return true;
            }
            else if (pedestals[index + 1].placedObject.name.ToLower() == personName)
            {
                return true;
            }
        }
        return false;
    }
    bool isNearTea(int index)
    {
        if (index == 5) return true;
        return false;
    }
    bool isNearCookies(int index)
    {
        if (index == 1) return true;
        return false;
    }


    public bool CheckSeating()
    {
        bool isGoodSeating = true;
        for (int i = 0; i < pedestals.Length; i++)
        {
            if (pedestals[i].placedObject == null) return false;
            Debug.Log(pedestals.Length);

            if (pedestals[i].placedObject.name.ToLower() == "F")
            {
                if (!isNearPerson(i, "F"))
                {
                    return false;
                }
            }
            if (pedestals[i].placedObject.name.ToLower() == "B")
            {
                if (!isNearCookies(i))
                {
                    return false;
                }
            }
            if (pedestals[i].placedObject.name.ToLower() == "C")
            {
                if (!isNearPerson(i, "A") && !isNearPerson(i, "H"))
                {
                    return false;
                }
            }
            if (pedestals[i].placedObject.name.ToLower() == "E")
            {
                if (!isNearPerson(i, "H"))
                {
                    return false;
                }
            }
            if (pedestals[i].placedObject.name.ToLower() == "F")
            {
                if (!isNearTea(i)) return false;
            }
            if (pedestals[i].placedObject.name.ToLower() == "G")
            {
                if (!(isNearPerson(i, "A") && isNearPerson(i, "C")))
                {
                    return false;
                }
            }
            if (pedestals[i].placedObject.name.ToLower() == "H")
            {
                if (!isNearPerson(i, "D"))
                {
                    return false;
                }
            }
        }
        if (pedestals.Length <= 0) isGoodSeating = false;
        return isGoodSeating;
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckSeating() && !isSeatedWell)
        {
            isSeatedWell = true;
           gameEvent.Raise();

        }
    }
}