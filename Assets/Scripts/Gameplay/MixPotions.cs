
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Yarn.Unity;

[System.Serializable]
public class Potion
{
    public Items.Ingredient[] ingredients1;
    public GameObject gameObject;
}
public class MixPotions : MonoBehaviour
{
    public GameEvent cantMix;
    public Potion[] PotionRecipes;
    public GameEvent mixedPotionsRight;
    public Pedestal[] pedestals;
    public GameObject spawnPotionLocation;
    bool madePotion = false;
    public UnityEvent botchedMix;

    
    IEnumerator DelayAction()
    {

        yield return new WaitForSeconds(.4f);
        botchedMix.Invoke();
    }
    public Items.Ingredient[] getPedestalIngredients(Pedestal[] pedistals)
    {
        Items.Ingredient[] ingredients = new Items.Ingredient[3];
        // string[] ingredients = new string[3];
        for (int i = 0; i < pedistals.Length; i++)
        {
            ingredients[i] = pedistals[i].placedObject.GetComponent<PickUp>().ingredientName;
        }
        return ingredients;
    }

    bool CheckMatchingPotionHelper(Potion potion, Items.Ingredient[] ingredients)
    {
        List<Items.Ingredient> list = new List<Items.Ingredient>();
        for (int i = 0; i < potion.ingredients1.Length; i++)
        {
            list.Add(potion.ingredients1[i]);
        }
        foreach (Items.Ingredient ingredient in ingredients)
        {
            if (list.Contains(ingredient)) list.Remove(ingredient);
        }
        if (list.Count == 0) return true;
        else return false;

    }
    [YarnCommand("CheckFull")]
    public bool ArePedestalesFull()
    {

        foreach (Pedestal pedestal in pedestals)
        {
            if (pedestal.placedObject == null)
            {

                FindObjectOfType<DialogueRunner>().variableStorage.SetValue("$enoughIngredients", false);
                return false;
            }
        }
        FindObjectOfType<DialogueRunner>().variableStorage.SetValue("$enoughIngredients", true);
        return true;
    }
    public void SpawnPotion(GameObject potion)
    {
        Instantiate(potion, spawnPotionLocation.transform).name = "Potion";
        // potion.SetActive(true);

    }
    public void ClearPotions()
    {

        foreach (Pedestal pedestal in pedestals)
        {
            if (pedestal.placedObject != null) Destroy(pedestal.placedObject);
        }
    }


    [YarnCommand("MixPotion")]
    public bool CheckMatchingPotion()
    {
        if (!ArePedestalesFull())
        {
            return false;
        }
        for (int i = 0; i < PotionRecipes.Length; i++)
        {
            if (CheckMatchingPotionHelper(PotionRecipes[i], getPedestalIngredients(pedestals)))
            {
                SpawnPotion(PotionRecipes[i].gameObject);
                ClearPotions();
                return true;
            }
        }
        ClearPotions();
        StartCoroutine("DelayAction");
        // botchedMix.Invoke();
        return false;
    }
}
