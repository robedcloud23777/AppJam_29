using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRod : Tool
{
    [SerializeField] GameObject m_fishingPin;
    public GameObject fishingPin => m_fishingPin;
}