using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PotionDisplay : MonoBehaviour
{
    public Potion potion;

    public Image artwork;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI valueText;

    public Potion upgradePotion;

    private void Start()
    {
        DisplayPotion(potion);
    }

    public void DisplayPotion(Potion currentPotion)
    {
        nameText.text = currentPotion.potionName;
        artwork.sprite = currentPotion.icon;
        //descriptionText.text = currentPotion.description;
        //valueText.text = currentPotion.value.ToString();
    }

}
