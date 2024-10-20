using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellPotion : MonoBehaviour
{
    public static Action<int> _itemIndex;

    private ItemDisplay itemInfo;
    private void Start()
    {
        itemInfo = GetComponent<ItemDisplay>();
    }
    public void Sell()
    {
        if (_itemIndex != null)
        {
            _itemIndex(itemInfo.itemIndex);
        }
    }
}
