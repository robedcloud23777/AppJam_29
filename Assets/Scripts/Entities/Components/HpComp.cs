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
        hp = maxHp;
    }
    public bool dead { get; private set; } = false;
    public DamageReceivedData GetDamage(DamageData damage)
    {
        if (dead) return new() { amount = 0 };

        DamageReceivedData data = new();

        data.amount = damage.amount;
        hp = Mathf.Max(0.0f, hp - damage.amount);
        if(hp <= 0.0f)
        {
            dead = true;
            OnDeath();
        }
        return new();
    }
    protected virtual void OnDeath() { }
}