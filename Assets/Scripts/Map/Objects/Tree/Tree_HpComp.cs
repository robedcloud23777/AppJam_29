using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree_HpComp : HpComp
{
    Tree origin;
    private void OnEnable()
    {
        Init();
    }
    public void OnAwake()
    {
        origin = GetComponent<Tree>();
    }
    protected override void OnDeath()
    {
        base.OnDeath();
        Player_Inventory player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Inventory>();
        foreach(var i in origin.loot.GenerateLoot())
        {
            player.Insert_DropRest(i.item, i.count);
        }
        origin.Release();
    }
    public override DamageReceivedData GetDamage(DamageData damage)
    {
        if (damage.toolType != ToolType.Axe) damage.amount /= 4.0f;
        return base.GetDamage(damage);
    }
}
