using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackableItemData : ItemData
{
    [Header("StackableItem")]
    [SerializeField] int m_maxStack = 64;
    public override int maxStack => m_maxStack;
    public override Item Create()
    {
        return new StackableItem(this);
    }
}
public class StackableItem : Item
{
    new public readonly StackableItemData data;
    public StackableItem(StackableItemData data) : base(data)
    {
        this.data = data;
    }
}