using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : Tool
{
    [SerializeField] TriggerCallbacks m_hitbox;
    public TriggerCallbacks hitbox => m_hitbox;
}