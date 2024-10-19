using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopDisplay : MonoBehaviour
{
    public TextMeshProUGUI itemTitle;
    public TextMeshProUGUI itemDescription;
    public TextMeshProUGUI itemPrice;
    public Image itemImage;
    public int itemIndex;

    public void DisplayInfo(int index, string title, string desc, string price, Sprite img)
    {
        itemIndex = index;
        itemTitle.text = title;
        //itemDescription.text = desc;
        itemPrice.text = price;
        itemImage.sprite = img;
    }
}
