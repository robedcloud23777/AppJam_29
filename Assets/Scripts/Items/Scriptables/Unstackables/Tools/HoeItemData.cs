using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

[CreateAssetMenu(fileName = "Hoe Item Data", menuName = "Scriptables/Items/Unstackables/Tools/Hoe", order = 0)]
public class HoeItemData : ToolItemData
{
    [Header("Hoe")]
    [SerializeField] TilledLand m_tilledLandPrefab;
    [SerializeField] CooldownSource m_tillCooldownSource;
    [SerializeField] float m_tillCooldown;
    public TilledLand tilledLandPrefab => m_tilledLandPrefab;
    public CooldownSource tillCooldownSource => m_tillCooldownSource;
    public float tillCooldown => m_tillCooldown;
    public override Item Create()
    {
        return new HoeItem(this);
    }
}
public class HoeItem : ToolItem, ICooldownDisplayed
{
    new public readonly HoeItemData data;
    public Hoe hoe => base.tool as Hoe;
    public HoeItem(HoeItemData data) : base(data)
    {
        this.data = data;
    }
    public float cooldownLeft => data.tillCooldownSource.cooldownLeft / data.tillCooldownSource.cooldown;
    public override void OnWieldUpdate()
    {
        base.OnWieldUpdate();
        if (Input.GetMouseButtonDown(0) && !data.tillCooldownSource.isOnCooldown)
        {
            Vector2 pos = new Vector2(Mathf.Round(wielder.transform.position.x), Mathf.Round(wielder.transform.position.y));
            if (!Physics2D.BoxCast(pos, new Vector2(0.49f, 0.49f), 0.0f, Vector2.up, 0.0f, LayerMask.GetMask("Map")))
            {
                Till(pos);
                data.tillCooldownSource.cooldown = data.tillCooldown;
                data.tillCooldownSource.cooldownLeft = data.tillCooldown;
                wielder.cooldowns.AddCooldown(data.tillCooldownSource);
            }
        }
    }
    void Till(Vector2 pos)
    {
        TilledLand tmp = data.tilledLandPrefab.Instantiate() as TilledLand;
        tmp.transform.position = pos;
    }
}