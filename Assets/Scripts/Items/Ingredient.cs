using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ingredient", menuName = "Ingredient")]
public class Ingredient : ScriptableObject
{
    public string ingredientName;
    public string description;
    public float value;
    public Sprite icon;

    public MergeTable mergeTable = new MergeTable();
}
