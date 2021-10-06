using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Potion
{
    public string[] ingredients;
    public GameObject gameObject;
}
public class MixPotions : MonoBehaviour
{
    public Potion[] PotionRecipes;
    public GameEvent mixedPotionsRight;
    public string[] concotionPassword = { "blue", "green", "yellow" };
    public Pedestal[] pedestals;
    bool madePotion = false;
    public string[] getPedestalIngredients(Pedestal[] pedistals)
    {
        string[] ingredients = new string[3];
        for (int i = 0; i < pedistals.Length; i++)
        {
            ingredients[i] += pedistals[i].placedObject.name;
        }
        return ingredients;
    }

    bool CheckMatchingPotionHelper(Potion potion, string[] ingredients)
    {
        List<string> list = new List<string>();
        for (int i = 0; i < potion.ingredients.Length; i++)
        {
            list.Add(potion.ingredients[i]);
        }
        foreach (string ingredient in ingredients)
        {
            for (int i = 0; i < potion.ingredients.Length; i++)
            {
                if (ingredient.ToLower() == potion.ingredients[i])
                {
                    list.Remove(ingredient);
                }
            }
        }
        if (list.Count == 0)
        {
            mixedPotionsRight.Raise();
            return true;
        }
        else return false;

    }

    public bool ArePedestalesFull()
    {
        foreach (Pedestal pedestal in pedestals)
        {
            if (pedestal.placedObject == null) return false;
        }
        return true;
    }
    public void SpawnPotion(GameObject potion)
    {
        potion.SetActive(true);

    }

    public bool CheckMatchingPotion()
    {
        if (!ArePedestalesFull()) return false;
        for (int i = 0; i < PotionRecipes.Length; i++)
        {
            if (CheckMatchingPotionHelper(PotionRecipes[i], getPedestalIngredients(pedestals)))
            {
                SpawnPotion(PotionRecipes[i].gameObject);
            }
        }
        return true;
    }

    // public bool CheckCorrectConcoction()
    // {
    //     List<string> list = new List<string>();
    //     for (int i = 0; i < concotionPassword.Length; i++)
    //     {
    //         list.Add(concotionPassword[i]);
    //     }
    //     foreach (Pedestal pedestal in pedestals)
    //     {
    //         if (pedestal.placedObject == null) return false;
    //         for (int i = 0; i < list.Count; i++)
    //         {
    //             if (pedestal.placedObject.name.ToLower() == list[i].ToLower())
    //             {
    //                 list.Remove(list[i]);
    //             }
    //         }
    //     }
    //     if (list.Count == 0)
    //     {
    //         mixedPotionsRight.Raise();
    //         return true;
    //     }
    //     else return false;
    // }

    // Update is called once per frame
    void Update()
    {
        if(!madePotion)
        {
            if(CheckMatchingPotion()){
                madePotion = true;
            }

        }
    }
}
