using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Crop : PooledPrefab<Crop>
{
    [SerializeField] GameObject sproutModel;
    [SerializeField] GrowthStage[] growthStages;
    [SerializeField] LootTable m_harvestLoot;
    [SerializeField] int m_harvestExperience;
    GameObject currentModel;
    public LootTable harvestLoot => m_harvestLoot;
    public int harvestExperience => m_harvestExperience;
    int currentStage = 0;
    public float growth { get; private set; } = 0;
    public bool mature => currentStage >= growthStages.Length;
    private void OnEnable()
    {
        ChangeModel(sproutModel);
        for (int i = 1; i < growthStages.Length; i++) growthStages[i].model.SetActive(false);
        currentStage = 0;
        growth = 0;
    }
    private void Update()
    {
        if(currentStage < growthStages.Length)
        {
            growth += Time.deltaTime;
            if(growth >= growthStages[currentStage].growth)
            {
                ChangeModel(growthStages[currentStage].model);
                currentStage++;
            }
        }
    }
    void ChangeModel(GameObject model)
    {
        if (currentModel != null) currentModel.SetActive(false);
        currentModel = model;
        if (currentModel != null) currentModel.SetActive(true);
    }
}
[System.Serializable]
public struct GrowthStage
{
    public float growth;
    public GameObject model;
}