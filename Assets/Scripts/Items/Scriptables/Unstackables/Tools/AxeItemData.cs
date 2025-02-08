using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

[CreateAssetMenu(fileName = "Axe Item Data", menuName = "Scriptables/Unstackables/Tools/Axe", order = 0)]
public class AxeItemData : ToolItemData
{
    [Header("Axe")]
    [SerializeField] CooldownSource m_attackCooldownSource;
    [SerializeField] float m_attackCooldown;
    public CooldownSource chopCooldownSource => m_attackCooldownSource;
    public float attackCooldown => m_attackCooldown;
    public override Item Create()
    {
        return new AxeItem(this);
    }
}
public class AxeItem : ToolItem, ICooldownDisplayed
{
    new public readonly AxeItemData data;
    public Axe axe => base.tool as Axe;
    public AxeItem(AxeItemData data) : base(data)
    {
        this.data = data;
    }
    public float cooldownLeft => data.chopCooldownSource.cooldownLeft / data.chopCooldownSource.cooldown;
    bool attacking = false;
    public override void OnWield(Player wielder)
    {
        base.OnWield(wielder);
        axe.hitbox.onTriggerStay += OnTriggerStay;
    }
    public override void OnWieldUpdate()
    {
        base.OnWieldUpdate();
        if(Input.GetMouseButtonDown(0) && !data.chopCooldownSource.isOnCooldown)
        {
            wielder.animation.TriggerAttack();
        }
    }
    public override void OnUnwield(Player wielder)
    {
        base.OnUnwield(wielder);
        axe.hitbox.onTriggerStay -= OnTriggerStay;
        attacking = false;
    }
    List<GameObject> hit = new();
    void OnAttackStart()
    {
        hit.Clear();
        attacking = true;
    }
    void OnAttackEnd()
    {
        attacking = false;
    }
    void OnTriggerStay(Collider2D other)
    {
        if (!attacking) return;
        GameObject hit = (other.attachedRigidbody == null ? other.gameObject : other.attachedRigidbody.gameObject);
    }
}