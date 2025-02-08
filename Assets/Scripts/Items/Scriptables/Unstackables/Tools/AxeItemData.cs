using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

[CreateAssetMenu(fileName = "Axe Item Data", menuName = "Scriptables/Items/Unstackables/Tools/Axe", order = 0)]
public class AxeItemData : ToolItemData
{
    [Header("Axe")]
    [SerializeField] float m_damage;
    [SerializeField] CooldownSource m_attackCooldownSource;
    [SerializeField] float m_attackCooldown;
    public float damage => m_damage;
    public CooldownSource attackCooldownSource => m_attackCooldownSource;
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
    public float cooldownLeft => data.attackCooldownSource.cooldownLeft / data.attackCooldownSource.cooldown;
    bool attacking = false;
    public override void OnWield(Player wielder)
    {
        base.OnWield(wielder);
        axe.hitbox.onTriggerStay += OnTriggerStay;
        wielder.animation.onAttackStart += OnAttackStart;
        wielder.animation.onAttackEnd += OnAttackEnd;
    }
    public override void OnWieldUpdate()
    {
        base.OnWieldUpdate();
        if(Input.GetMouseButtonDown(0) && UIScanner.ScanUI().Count <= 0 && !data.attackCooldownSource.isOnCooldown)
        {
            wielder.animation.TriggerAttack();
            data.attackCooldownSource.cooldown = data.attackCooldown;
            data.attackCooldownSource.cooldownLeft = data.attackCooldown;
            wielder.cooldowns.AddCooldown(data.attackCooldownSource);
        }
    }
    public override void OnUnwield(Player wielder)
    {
        base.OnUnwield(wielder);
        axe.hitbox.onTriggerStay -= OnTriggerStay;
        wielder.animation.onAttackStart -= OnAttackStart;
        wielder.animation.onAttackEnd -= OnAttackEnd;
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
        GameObject hitObject = (other.attachedRigidbody == null ? other.gameObject : other.attachedRigidbody.gameObject);
        if (hit.Contains(hitObject)) return;
        hit.Add(hitObject);
        if(hitObject.TryGetComponent(out IDamagable tmp))
        {
            tmp.GetDamage(new()
            {
                amount = data.damage,
                toolType = ToolType.Axe
            });
        }
    }
}