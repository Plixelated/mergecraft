using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Merges
{
    public List<ScriptableObject> validMerge;
    public Potion output;
}
[System.Serializable]
public class MergeTable
{
    public List<Merges> recipes;
}
