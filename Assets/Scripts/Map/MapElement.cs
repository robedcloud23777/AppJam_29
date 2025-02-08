using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MapElement : PooledPrefab<MapElement>
{
    [Header("Pre-Instanced")]
    [SerializeField] MapElement m_prefabOrigin;
    private void Awake()
    {
        prefabOrigin = m_prefabOrigin;
    }
    private void OnEnable()
    {
        MapManager.Instance.AddElement(this);
    }
    private void OnDisable()
    {
        MapManager.Instance.RemoveElement(this);
    }
    public virtual void Save(DataUnit data)
    {
        data.floats["posX"] = transform.position.x;
        data.floats["posY"] = transform.position.y;
    }
    public virtual void Load(DataUnit data)
    {
        Vector2 pos = new();
        if (data.floats.TryGetValue("posX", out float x)) pos.x = x;
        if (data.floats.TryGetValue("poxY", out float y)) pos.y = y;
        transform.position = pos;
    }
}