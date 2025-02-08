using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Seed Item Data", menuName = "Scriptables/Items/Stackables/Seeds", order = 0)]
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
    bool seeding = false;
    const float seedingSpeedMultiplier = 0.75f;
    public override void OnWieldUpdate()
    {
        base.OnWieldUpdate();
        if (Input.GetMouseButton(0) && UIScanner.ScanUI().Count <= 0)
        {
            if (!seeding)
            {
                seeding = true;
                wielder.movement.speedMultiplier *= seedingSpeedMultiplier;
            }
        }
        else
        {
            if (seeding)
            {
                seeding = false;
                wielder.movement.speedMultiplier /= seedingSpeedMultiplier;
            }
        }

        if (seeding)
        {
            foreach(var i in Physics2D.OverlapPointAll(wielder.transform.position, LayerMask.GetMask("Map")))
            {
                GameObject hitObject = (i.attachedRigidbody == null ? i.gameObject : i.attachedRigidbody.gameObject);
                if(hitObject.TryGetComponent(out TilledLand land))
                {
                    if(land.plantedCrop == null)
                    {
                        Crop crop = data.plantingCrop.Instantiate();
                        crop.transform.position = land.transform.position;
                        land.plantedCrop = crop;
                        containedSlot.count--;
                        break;
                    }
                }
            }
        }
    }
    public override void OnUnwield(Player wielder)
    {
        base.OnUnwield(wielder);
        if (seeding)
        {
            seeding = false;
            wielder.movement.speedMultiplier /= seedingSpeedMultiplier;
        }
    }
    public override AnimationType heldAnimation => base.heldAnimation;
}