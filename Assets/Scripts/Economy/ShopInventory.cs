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
                newItem.GetComponent<ItemDisplay>().DisplayInfo(i,newPotion.potionName, newPotion.description, newPotion.value.ToString(), newPotion.icon);
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

        UpdateInventory();
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
        Potion item = null;
        if (shopInventory[index] is Potion)
        {
            item = (Potion)shopInventory[index];
        }
        Debug.Log(item.value);
        //Add SO for Ingredient here in else statement

        if(_buyItem != null)
            _buyItem(item.value);

        purchasedItems.Add(index);

        if(_purchasedItem != null)
            _purchasedItem(shopInventory[index]);

        inventoryCount -= 1;
    }


}
