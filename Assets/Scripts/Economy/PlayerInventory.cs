using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerInventory : MonoBehaviour
{
    public List<ScriptableObject> playerInventory = new List<ScriptableObject>();
    private int inventoryCount;

    [SerializeField] private GameObject playerItem;
    [SerializeField] private Transform itemDisplay;

    private void OnEnable()
    {
        ShopInventory._purchasedItem += AddToInventory;
        UIManager._potionMenuOpen += PopulateInventory;
        UIManager._potionMenuClose += ClearInventory;
        MergeItem._merge += MergeItems;
    }
    private void OnDisable()
    {
        ShopInventory._purchasedItem -= AddToInventory;
        UIManager._potionMenuOpen -= PopulateInventory;
        UIManager._potionMenuClose -= ClearInventory;
        MergeItem._merge -= MergeItems;
    }

    private void AddToInventory(ScriptableObject item)
    {
        playerInventory.Add(item);
    }

    private void PopulateInventory()
    {
        for (int i = 0; i < playerInventory.Count; i++)
        {
            if (playerInventory[i] is Potion)
            {
                inventoryCount += 1;
                GameObject newItem = (GameObject)Instantiate(playerItem, itemDisplay);
                Potion newPotion = (Potion)playerInventory[i];
                newItem.GetComponent<ItemDisplay>().DisplayInfo(i, newPotion.potionName, newPotion.description, newPotion.value.ToString(), newPotion.icon);
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
        //purchasedItems.Clear();
    }

    private void MergeItems(int mergeIndex, int placeIndex)
    {
        if (playerInventory[placeIndex] is Potion)
        {
            var newPotion = (Potion)playerInventory[placeIndex];
            playerInventory.RemoveAt(mergeIndex);
            //playerInventory[placeIndex] = newPotion.upgradePotion;
            inventoryCount -= 1;
            UpdatePotion(placeIndex);
        }

        
    }

    private void UpdatePotion(int placeIndex)
    {
        Potion newPotion = (Potion)playerInventory[placeIndex];

        for(int i = 0;i< itemDisplay.childCount;i++)
        {
            Transform child = itemDisplay.GetChild(i);
            if (child != null)
            {
                ItemDisplay itemDisplay = child.GetComponent<ItemDisplay>();
                if (itemDisplay.itemIndex == placeIndex)
                {
                    itemDisplay.DisplayInfo(placeIndex, newPotion.potionName, newPotion.description, newPotion.value.ToString(), newPotion.icon);
                }
            }

        }
        
    }

}
