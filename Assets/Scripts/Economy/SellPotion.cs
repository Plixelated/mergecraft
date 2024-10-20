using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellPotion : MonoBehaviour
{
    public static Action<int> _itemIndex;
    public static Action _sellSFX;
    private ItemDisplay itemInfo;

    private void Start()
    {
        itemInfo = GetComponent<ItemDisplay>();
    }
    public void Sell()
    {
        if (_itemIndex != null)
        {
            if(_sellSFX != null)
                _sellSFX();

            _itemIndex(itemInfo.itemIndex);
        }
    }
}
