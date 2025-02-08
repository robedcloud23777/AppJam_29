using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour, ISavable
{
    bool loaded = false;
    void Start()
    {
        if (!loaded) GenerateMap();
    }
    void GenerateMap()
    {

    }
    public void Load(SaveData data)
    {
        loaded = true;

    }

    public void Save(SaveData data)
    {

    }
}
