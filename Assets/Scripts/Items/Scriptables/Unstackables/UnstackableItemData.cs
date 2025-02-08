using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Unstackable Item Data", menuName = "Scriptables/Unstackables/Item", order = 0)]
public class UnstackableItemData : ItemData
{
    public override int maxStack => 1;
    public override Item Create()
    {
        return new UnstackableItem(this);
    }
}
public class UnstackableItem : Item
{
    new public readonly UnstackableItemData data;
    public UnstackableItem(UnstackableItemData data) : base(data)
    {
        this.data = data;
    }
    public override bool IsStackable(Item other) => false;
}