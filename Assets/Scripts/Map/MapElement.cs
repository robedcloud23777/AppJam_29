using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MapElement : PooledPrefab<MapElement>
{
    public virtual void Save(DataUnit data) { }
    public virtual void Load(DataUnit data) { }
}