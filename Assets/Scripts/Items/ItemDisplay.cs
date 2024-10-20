using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
    public TextMeshProUGUI itemTitle;
    public TextMeshProUGUI itemPrice;
    public Image itemImage;
    public int itemIndex;
    public GameObject sellButton;

    public void DisplayInfo(int index, string title, string desc, string price, Sprite img, bool isPotion)
    {
        itemIndex = index;
        itemTitle.text = title;
        itemImage.sprite = img;
        //Optional Values
        if (itemPrice != null)
            itemPrice.text = price.ToString();
        if (sellButton != null)
            sellButton.SetActive(isPotion);
    }
}
