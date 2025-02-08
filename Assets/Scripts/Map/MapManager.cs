using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour, ISavable
{
    public static MapManager Instance { get; private set; }
    public MapManager() => Instance = this;

    [SerializeField] MapElement[] startElements;
    List<MapElement> mapElements = new();
    bool loaded = false;
    void Start()
    {
        if (loaded) foreach (var i in startElements) i.gameObject.SetActive(false);
    }
    Dictionary<string, MapElement> resourceLoaded = new();
    public void Load(SaveData data)
    {
        loaded = true;
        foreach(var i in data.map)
        {
            if(i.strings.TryGetValue("prefab", out string tmp))
            {
                if (!resourceLoaded.ContainsKey(tmp)) resourceLoaded.Add(tmp, Resources.Load<MapElement>("Map/" + tmp));
                MapElement prefab = resourceLoaded[tmp];
                if(prefab != null)
                {
                    MapElement tmp2 = prefab.Instantiate();
                    tmp2.Load(i);
                }
            }
        }
    }

    public void Save(SaveData data)
    {
        
        foreach(var i in mapElements)
        {
            if (i.prefabOrigin == null) continue;
            DataUnit tmp = new();
            tmp.strings["prefab"] = i.prefabOrigin.name;
            i.Save(tmp);
            data.map.Add(tmp);
        }
    }
    public void AddElement(MapElement element)
    {
        if (mapElements.Contains(element)) return;
        mapElements.Add(element);
    }
    public void RemoveElement(MapElement element)
    {
        if(!mapElements.Contains(element)) return;
        mapElements.Remove(element);
    }
}
[System.Serializable]
public class MapElementWrapper
{
    public MapElement element;
}
