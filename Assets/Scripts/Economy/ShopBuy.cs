using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBuy : MonoBehaviour
{
    public static Action<int> _itemIndex;
    public static Action _sellSFX;
    private float playerBalance;
    private ItemDisplay itemInfo;

    private void OnEnable()
    {
        EconomyManager._playerBalance += GetBalance;
    }

    private void OnDisable()
    {
        EconomyManager._playerBalance -= GetBalance;
    }

    private void GetBalance(float balance)
    {
        playerBalance = balance;
    }
    private void Start()
    {
        itemInfo = GetComponent<ItemDisplay>();
    }
    public void Buy()
    {
        int price = Int32.Parse(itemInfo.itemPrice.text);

        float newBalance = playerBalance - price;

        if (newBalance >= 0)
        {
            if (_itemIndex != null)
            {
                if (_sellSFX != null)
                    _sellSFX();
                _itemIndex(itemInfo.itemIndex);
            }
        }
    }
}
