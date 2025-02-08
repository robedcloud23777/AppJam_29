using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

[CreateAssetMenu(fileName = "Sickle Item Data", menuName = "Scriptables/Items/Unstackables/Tools/Sickle", order = 0)]
public class SickleItemData : ToolItemData
{
    [Header("Sickle")]
    [SerializeField] CooldownSource m_harvestCooldownSource;
    [SerializeField] float m_harvestCooldown;
    public CooldownSource harvestCooldownSource => m_harvestCooldownSource;
    public float harvestCooldown => m_harvestCooldown;
    public override Item Create()
    {
        return new SickleItem(this);
    }
}
public class SickleItem : ToolItem, ICooldownDisplayed
{
    new public readonly SickleItemData data;
    public Sickle Sickle => base.tool as Sickle;
    public SickleItem(SickleItemData data) : base(data)
    {
        this.data = data;
    }
    public float cooldownLeft => data.harvestCooldownSource.cooldownLeft / data.harvestCooldownSource.cooldown;
    public override void OnWieldUpdate()
    {
        base.OnWieldUpdate();
        if (Input.GetMouseButton(0) && UIScanner.ScanUI().Count <= 0 && !data.harvestCooldownSource.isOnCooldown)
        {
            foreach (var i in Physics2D.OverlapPointAll(wielder.transform.position, LayerMask.GetMask("Map")))
            {
                GameObject hitObject = (i.attachedRigidbody == null ? i.gameObject : i.attachedRigidbody.gameObject);
                if (hitObject.TryGetComponent(out TilledLand land))
                {
                    if (land.plantedCrop != null)
                    {
                        if (land.plantedCrop.mature)
                        {
                            foreach(var k in land.plantedCrop.harvestLoot.GenerateLoot())
                            {
                                wielder.inventory.Insert_DropRest(k.item, k.count);
                            }
                        }
                        land.plantedCrop.Release();
                        land.plantedCrop = null;
                        data.harvestCooldownSource.cooldown = data.harvestCooldown;
                        data.harvestCooldownSource.cooldownLeft = data.harvestCooldown;
                        wielder.cooldowns.AddCooldown(data.harvestCooldownSource);
                        break;
                    }
                }
            }
        }
    }
}