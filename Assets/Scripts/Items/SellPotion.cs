using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellPotion : MonoBehaviour
{
    public static Action<Index> _sellPotion;

    public void OnSell()
    {
        var index = GetComponent<ItemDisplay>().itemIndex;
        if(_sellPotion != null )
            _sellPotion(index);
    }
}
