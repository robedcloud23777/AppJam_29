using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour, ISavable
{
    [SerializeField] MapElement[] startElements;
    List<MapElement> mapElements = new();
    bool loaded = false;
    void Start()
    {
        if (!loaded)
        {
            foreach(var i in startElements)
            {
                mapElements.Add(i);
            }
        }
        else
        {
            foreach (var i in startElements) i.gameObject.SetActive(false);
        }
    }
    public void Load(SaveData data)
    {
        loaded = true;

    }

    public void Save(SaveData data)
    {

    }
}
