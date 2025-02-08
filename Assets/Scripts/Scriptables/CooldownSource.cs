using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CooldownSource", menuName = "Scriptables/Cooldown Source", order = 0)]
public class CooldownSource : ScriptableObject
{
    internal float cooldown, cooldownLeft;
    internal bool isOnCooldown => cooldown > 0;
}