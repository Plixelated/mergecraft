using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ShopInventory : MonoBehaviour
{
    public List<ScriptableObject> shopInventory;
    public GameObject shopObject;
    public Transform itemDisplay;
    public int inventoryCount;
    public List<int> purchasedItems = new List<int>();

    public static Action<float> _buyItem;
    public static Action<ScriptableObject> _purchasedItem;

    private void OnEnable()
    {
        ShopBuy._itemIndex += GetItemIndex;
        UIManager._shopMenuOpen += PopulateInventory;
        UIManager._shopMenuClose += ClearInventory;
    }

    private void OnDisable()
    {
        ShopBuy._itemIndex -= GetItemIndex;
        UIManager._shopMenuOpen -= PopulateInventory;
        UIManager._shopMenuClose -= ClearInventory;
    }
    private void PopulateInventory()
    {
        for(int i = 0; i < shopInventory.Count; i++)
        {
            if (shopInventory[i] is Potion)
            {
                inventoryCount += 1;
                GameObject newItem = (GameObject)Instantiate(shopObject, itemDisplay);
                Potion newPotion = (Potion)shopInventory[i];
                newItem.GetComponent<ItemDisplay>().DisplayInfo(i,newPotion.potionName, newPotion.description, newPotion.value.ToString(), newPotion.icon, true);
            }
            else if (shopInventory[i] is Ingredient)
            {
                inventoryCount += 1;
                GameObject newItem = (GameObject)Instantiate(shopObject, itemDisplay);
                Ingredient newIngredient = (Ingredient)shopInventory[i];
                newItem.GetComponent<ItemDisplay>().DisplayInfo(i, newIngredient.ingredientName, newIngredient.description, newIngredient.value.ToString(), newIngredient.icon, false);
            }
        }
    }

    private void ClearInventory()
    {
        for (int i = 0; i < itemDisplay.childCount; i++)
        {
            GameObject child = itemDisplay.GetChild(i).gameObject;
            Destroy(child);
        }

        //UpdateInventory();
        purchasedItems.Clear();
    }

    private void UpdateInventory()
    {
        var sortedItems = purchasedItems.OrderBy(n => n).ToList();
        for( int i = 0; i <sortedItems.Count; i++)
        {
            shopInventory.RemoveAt(sortedItems[i]);
            for(int j = 0; j < sortedItems.Count; j++)
            {
                sortedItems[j] -= 1;
            }
            
        }
        inventoryCount = 0;
    }

    private void GetItemIndex(int index)
    {
        
        if (shopInventory[index] is Potion)
        {
            var item = (Potion)shopInventory[index];
            if (_buyItem != null)
                _buyItem(item.value);
        }
        else if (shopInventory[index] is Ingredient)
        {
            var item = (Ingredient)shopInventory[index];
            if (_buyItem != null)
                _buyItem(item.value);
        }


        purchasedItems.Add(index);

        if(_purchasedItem != null)
            _purchasedItem(shopInventory[index]);

        //inventoryCount -= 1;
    }


}
