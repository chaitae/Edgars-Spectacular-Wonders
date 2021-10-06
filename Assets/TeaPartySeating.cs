using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeaPartySeating : MonoBehaviour
{
    public Pedestal[] pedestals;
    bool isSeatedWell = false;

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
            if(pedestals[i].placedObject == null) return false;
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
               if(!isNearTea(i)) return false;
            }
            if(pedestals[i].placedObject.name.ToLower() == "G")
            {
                if(!(isNearPerson(i,"A") && isNearPerson(i,"C")))
                {
                    return false;
                }
            }
            if(pedestals[i].placedObject.name.ToLower() == "H")
            {
                if(!isNearPerson(i,"D"))
                {
                    return false;
                }
            }
        }
        if(pedestals.Length <= 0) isGoodSeating = false;
        return isGoodSeating;
    }
    void OnSeatedWell()
    {
        foreach(Pedestal pedestal in pedestals)
        {
            pedestal.enabled = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(CheckSeating()  && !isSeatedWell)
        {
            isSeatedWell = true;
        }
    }
}
