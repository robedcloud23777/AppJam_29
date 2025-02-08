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
    private void Awake()
    {
        hpComp = GetComponent<Tree_HpComp>();
        hpComp.OnAwake();
    }
}
