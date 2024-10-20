using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<ScriptableObject> playerInventory = new List<ScriptableObject>();
    private int inventoryCount;

    [SerializeField] private GameObject playerItem;
    [SerializeField] private Transform itemDisplay;

    public static Action<int> _invalidMerge;
    public static Action<int> _validMerge;

    [SerializeField] private Potion finalPotion;

    public bool isOpen;

    public static Action _gameOver;

    private void OnEnable()
    {
        ShopInventory._purchasedItem += AddToInventory;
        UIManager._potionMenuOpen += PopulateInventory;
        UIManager._potionMenuClose += ClearInventory;
        MergeItem._merge += MergeItems;
        ForagingManager._ingredient += AddToInventory;
    }
    private void OnDisable()
    {
        ShopInventory._purchasedItem -= AddToInventory;
        UIManager._potionMenuOpen -= PopulateInventory;
        UIManager._potionMenuClose -= ClearInventory;
        MergeItem._merge -= MergeItems;
        ForagingManager._ingredient -= AddToInventory;
    }

    private void AddToInventory(ScriptableObject item)
    {
        if(item == finalPotion)
        {
            if(_gameOver != null)
            {
                _gameOver();
            }
        }

        playerInventory.Add(item);

        if(isOpen)
        {
           AddIngredient(playerInventory.Count-1);
        }

    }

    private void AddIngredient(int i)
    {
        inventoryCount += 1;
        GameObject newItem = (GameObject)Instantiate(playerItem, itemDisplay);
        Ingredient newIngredient = (Ingredient)playerInventory[i];
        newItem.GetComponent<ItemDisplay>().DisplayInfo(i, newIngredient.ingredientName, newIngredient.description, newIngredient.value.ToString(), newIngredient.icon);
    }

    private void AddPotion(int i)
    {
        inventoryCount += 1;
        GameObject newItem = (GameObject)Instantiate(playerItem, itemDisplay);
        Potion newPotion = (Potion)playerInventory[i];
        newItem.GetComponent<ItemDisplay>().DisplayInfo(i, newPotion.potionName, newPotion.description, newPotion.value.ToString(), newPotion.icon);
    }

    private void PopulateInventory()
    {
        isOpen = true;

        for (int i = 0; i < playerInventory.Count; i++)
        {
            if (playerInventory[i] is Potion)
            {
                AddPotion(i);
            }
            else if (playerInventory[i] is Ingredient)
            {
                AddIngredient(i);
            }
        }
    }

    private void MergeItems(int mergeIndex, int targetIndex)
    {
        var targetItem = playerInventory[targetIndex];
        var currentItem = playerInventory[mergeIndex];

        if (ValidateMerge(currentItem, targetItem)){

            if (_validMerge != null)
                _validMerge(mergeIndex);

            playerInventory[targetIndex] = GetOutput(currentItem, targetItem);
            playerInventory.RemoveAt(mergeIndex);
            inventoryCount -= 1;

            itemDisplay.GetChild(mergeIndex).gameObject.SetActive(false);
            itemDisplay.GetChild(mergeIndex).SetAsLastSibling();

            UpdatePotion();
        }
        else
        {
            if (_invalidMerge != null)
            {
                _invalidMerge(mergeIndex);
            }
        }

    }
    private MergeTable GetMergeTable(ScriptableObject obj)
    {
        if (obj is Potion)
        {
            Potion potion = (Potion)obj;
            return potion.mergeTable;
        }
        else if (obj is Ingredient)
        {
            Ingredient ingredient = (Ingredient)obj;
            return ingredient.mergeTable;
        }

        return null;
    }

    private ScriptableObject GetOutput(ScriptableObject mergeItem, ScriptableObject targetItem)
    {
        MergeTable mergeTable = GetMergeTable(mergeItem);
        bool isValid = false;

        if (targetItem != null && mergeItem != null)
        {
            foreach (var recipie in mergeTable.recipes)
            {
                foreach (var validMergeItem in recipie.validMerge)
                {
                    if (validMergeItem == targetItem)
                    {
                        isValid = true;
                    }
                    if (validMergeItem != targetItem)
                    {
                        isValid = false;
                    }
                }

                if(isValid)
                    return recipie.output;
            }
        }
        return null;
    }


    private bool ValidateMerge(ScriptableObject mergeItem, ScriptableObject targetItem)
    {
        bool isValid = false;

        MergeTable mergeTable = GetMergeTable(mergeItem);

        if (targetItem != null && mergeItem != null)
        {
            //Search Recipies
            foreach (var recipie in mergeTable.recipes)
            {
                //Validate Items in Recipes
                foreach (var validMergeItem in recipie.validMerge)
                {
                    if (validMergeItem == targetItem)
                    {
                        isValid = true;
                    }
                    if (validMergeItem != targetItem)
                    {
                        isValid = false;
                    }
                }

                if (isValid)
                    return isValid;

            }
        }

        return isValid;
    }

    private void UpdatePotion()
    {

        //Potion newPotion;
        Potion currentPotion = null;
        int activeIndex = 0;

        for (int i = 0; i < itemDisplay.childCount; i++)
        {
            Transform child = itemDisplay.GetChild(i);
            if (child != null && child.gameObject.activeSelf)
            {
                ItemDisplay itemDisplay = child.GetComponent<ItemDisplay>();
                //Update Index
                itemDisplay.itemIndex = activeIndex;
                try
                {
                    currentPotion = (Potion)playerInventory[activeIndex];
                }
                catch (Exception e)
                {
                    Debug.Log(e);

                }


                itemDisplay.DisplayInfo(activeIndex, currentPotion.potionName, currentPotion.description, currentPotion.value.ToString(), currentPotion.icon);
                activeIndex += 1;
            }

        }
    }

    private void ClearInventory()
    {
        isOpen = false;
        for (int i = 0; i < itemDisplay.childCount; i++)
        {
            GameObject child = itemDisplay.GetChild(i).gameObject;
            Destroy(child);
        }

        //UpdateInventory();
        //purchasedItems.Clear();
    }

}
