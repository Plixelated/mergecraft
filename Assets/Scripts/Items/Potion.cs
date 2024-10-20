using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Potion",menuName ="Potion")]
public class Potion : ScriptableObject
{
    public string potionName;
    public string description;
    public float value;
    public Sprite icon;
    public Sprite iconFill;

    public MergeTable mergeTable = new MergeTable();

}
