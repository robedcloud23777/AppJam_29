using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CooldownSource", menuName = "Scriptables/Cooldown Source", order = 0)]
public class CooldownSource : ScriptableObject
{
    [SerializeField] internal float cooldown = 1.0f;
    [SerializeField] internal float cooldownLeft = 0.0f;
    internal bool isOnCooldown => cooldownLeft > 0.0f;
}