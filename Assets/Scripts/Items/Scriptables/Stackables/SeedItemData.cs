using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Seed Item Data", menuName = "Scriptables/Seeds/Item", order = 0)]
public class SeedItemData : StackableItemData
{
    [Header("Seed")]
    [SerializeField] Crop m_plantingCrop;
    public Crop plantingCrop => m_plantingCrop;
    public override Item Create()
    {
        return new SeedItem(this);
    }
}
public class SeedItem : StackableItem
{
    new public readonly SeedItemData data;
    public SeedItem(SeedItemData data) : base(data)
    {
        this.data = data;
    }
    public override AnimationType heldAnimation => base.heldAnimation;
}