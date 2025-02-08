using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Loot Table", menuName = "Scriptables/Loot Table", order = 0)]
public class LootTable : ScriptableObject
{
    [SerializeField] LootElement[] lootTable;
    public IEnumerable<ItemIntPair> GenerateLoot()
    {
        foreach (var i in lootTable)
        {
            if (UnityEngine.Random.Range(0.0f, 100.0f) < i.chance)
            {
                yield return new()
                {
                    item = i.data.Create(),
                    count = UnityEngine.Random.Range(i.minCount, i.maxCount+1)
                };
            }
        }
    }
}
[System.Serializable]
public struct LootElement
{
    public float chance;
    public ItemData data;
    public int minCount, maxCount;
}