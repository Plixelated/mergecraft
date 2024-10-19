using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MergeTable {

    [System.Serializable]
    public struct rowData
    {
        public Potion row;
    }

    public rowData[] rows = new rowData[10];
}
