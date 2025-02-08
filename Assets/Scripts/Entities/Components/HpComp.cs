using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HpComp : MonoBehaviour, IDamagable
{
    [Header("HpComp")]
    [SerializeField] float m_maxHp;
    public float maxHp => m_maxHp;
    public float hp { get; private set; }

    public Action<DamageReceivedData> onDamage;
    internal void Init()
    {
        dead = false;
        hp = maxHp;
    }
    public bool dead { get; private set; } = false;
    public virtual DamageReceivedData GetDamage(DamageData damage)
    {
        if (dead || damage.amount <= 0) return new() { amount = 0 };

        DamageReceivedData data = new();

        data.amount = damage.amount;
        hp = Mathf.Max(0.0f, hp - damage.amount);
        if(hp <= 0.0f)
        {
            dead = true;
            OnDeath();
        }
        onDamage?.Invoke(data);
        return new();
    }
    public void SetHp(float hp)
    {
        hp = Mathf.Clamp(hp, 0.0f, maxHp);
        if(hp <= 0.0f)
        {
            dead = true;
            OnDeath();
        }
    }
    protected virtual void OnDeath() { }
}