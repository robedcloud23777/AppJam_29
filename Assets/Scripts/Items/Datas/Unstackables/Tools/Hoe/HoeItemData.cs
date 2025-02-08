using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoeItemData : ToolItemData
{
    [Header("Hoe")]
    [SerializeField] float m_tillCooldown;
    public float tillCooldown => m_tillCooldown;
    public override Item Create()
    {
        return new HoeItem(this);
    }
}
public class HoeItem : ToolItem
{
    new public readonly HoeItemData data;
    public Hoe hoe => base.tool as Hoe;
    public HoeItem(HoeItemData data) : base(data)
    {
        this.data = data;
    }
    float counter = 0.0f;
    public override void OnWield(Player wielder)
    {
        base.OnWield(wielder);
        counter = 0.0f;
    }
    public override void OnWieldUpdate()
    {
        base.OnWieldUpdate();
        if (counter < data.tillCooldown) counter += Time.deltaTime;
        if(Input.GetMouseButtonDown(0) && counter >= data.tillCooldown)
        {
            Till();
            counter = 0.0f;
        }
    }
    void Till()
    {

    }
}