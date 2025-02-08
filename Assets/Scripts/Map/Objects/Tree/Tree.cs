using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Tree_HpComp))]
public class Tree : MapElement
{
    [SerializeField] LootTable m_loot;
    public LootTable loot => m_loot;
    Tree_HpComp hpComp;
    private void OnEnable()
    {
        MapManager.Instance.AddElement(this);
    }
    private void Awake()
    {
        hpComp = GetComponent<Tree_HpComp>();
        hpComp.OnAwake();
    }
    public override void Save(DataUnit data)
    {
        base.Save(data);
        data.floats["hp"] = hpComp.hp;
    }
    public override void Load(DataUnit data)
    {
        base.Load(data);
        if (data.floats.TryGetValue("hp", out float tmp)) hpComp.SetHp(tmp);
    }
}
