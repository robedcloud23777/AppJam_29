using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stackable Item Data", menuName = "Scriptables/Items/Stackables/Item", order = 0)]
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