using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBuy : MonoBehaviour
{
    public static Action<int> itemIndex;

    private ShopDisplay itemInfo;

    private void Start()
    {
        itemInfo = GetComponent<ShopDisplay>();
    }
    public void Buy()
    {
        if (itemIndex != null)
        {
            itemIndex(itemInfo.itemIndex);
        }
        this.gameObject.SetActive(false);
    }
}
