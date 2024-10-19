using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New Potion",menuName ="Potion")]

[System.Serializable]
public class Outputs
{
    public Potion output;
}
[System.Serializable]
public class Merges
{
    public List<Potion> validMerge;
    public Outputs mergeResult;
}
[System.Serializable]
public class MergeList
{
    public List<Merges> mergeList;
}
public class Potion : ScriptableObject
{
    public string potionName;
    public string description;
    public float value;
    public Sprite icon;

    public MergeList mergeTable = new MergeList();

}
