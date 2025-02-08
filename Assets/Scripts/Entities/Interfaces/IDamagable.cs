using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    public DamageReceivedData GetDamage(DamageData damage);
}
public struct DamageData
{
    public float amount;
    public ToolType toolType;

    public static implicit operator DamageData(float amount)
    {
        return new DamageData() { amount = amount };
    }
}
public enum ToolType
{
    None = 0,
    Axe = 1
}
public struct DamageReceivedData
{
    public float amount;
    public bool fatal;
}