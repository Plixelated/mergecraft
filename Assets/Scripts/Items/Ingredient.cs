using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : ScriptableObject
{
    public string ingredientName;
    public string description;
    public float value;
    public Sprite icon;

    public Potion upgradeingredient;
}
