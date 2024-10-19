using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBuy : MonoBehaviour
{
    public static Action<int> _itemIndex;

    private ItemDisplay itemInfo;

    private void Start()
    {
        itemInfo = GetComponent<ItemDisplay>();
    }
    public void Buy()
    {
        if (_itemIndex != null)
        {
            _itemIndex(itemInfo.itemIndex);
        }
        this.gameObject.SetActive(false);
    }
}
