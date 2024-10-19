using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInventory : MonoBehaviour
{
    public List<ScriptableObject> shopInventory;
    public GameObject shopObject;
    public Transform itemDisplay;
    public int inventoryCount;
    public List<int> purchasedItems = new List<int>();

    public static Action<float> buyItem;

    private void OnEnable()
    {
        ShopBuy.itemIndex += GetItemIndex;
        PopulateInventory();
    }

    private void OnDisable()
    {
        ShopBuy.itemIndex -= GetItemIndex;

        for(int i = 0; i < itemDisplay.childCount; i++)
        {
            GameObject child = itemDisplay.GetChild(i).gameObject;
            Destroy(child);
        }

        UpdateInventory();
        purchasedItems.Clear();
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
                newItem.GetComponent<ShopDisplay>().DisplayInfo(i,newPotion.potionName, newPotion.description, newPotion.value.ToString(), newPotion.icon);
            }
        }
    }

    private void UpdateInventory()
    {
        for( int i = 0; i < purchasedItems.Count; i++)
        {
            shopInventory.RemoveAt(purchasedItems[i]);
            for(int j = 0; j < purchasedItems.Count; j++)
            {
                purchasedItems[j] -= 1;
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
        //Add SO for Item here in else statement
        if(buyItem != null)
            buyItem(item.value);

        purchasedItems.Add(index);

        inventoryCount -= 1;
    }


}
