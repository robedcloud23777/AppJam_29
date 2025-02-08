using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public DataUnit player = new();
    public List<DataUnit> map = new();
    public List<DataUnit> droppedItems = new();
}