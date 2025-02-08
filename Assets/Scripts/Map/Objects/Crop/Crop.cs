using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : PooledPrefab<Crop>
{
    [SerializeField] Animator anim;
    [SerializeField] float[] growthStages;
    int currentStage = 0;
    public float growth { get; private set; } = 0;
    public bool mature => currentStage >= growthStages.Length;
    readonly int growthStageID = Animator.StringToHash("GrowthStage");
    private void Update()
    {
        if(currentStage < growthStages.Length)
        {
            growth += Time.deltaTime;
            if(growth >= growthStages[currentStage + 1])
            {
                currentStage++;
                anim.SetInteger(growthStageID, currentStage);
            }
        }
    }
}
[System.Serializable]
public struct GrowthStage
{
    public float growth;
    public GameObject model;
}