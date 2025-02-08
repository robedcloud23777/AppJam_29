using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

#if UNITY_EDITOR
using UnityEditor;
#endif
public class Player_Statistics : MonoBehaviour, ISavable
{
    [SerializeField] int m_money = 0;
    public Action onMoneyChange;
    public int money
    {
        get => m_money;
        set
        {
            m_money = value;
            onMoneyChange?.Invoke();
        }
    }
    [SerializeField] int m_chillLevel = 1;
    public Action onChillLevelChange;
    public int chillLevel
    {
        get => m_chillLevel;
        set
        {
            m_chillLevel = value;
            onChillLevelChange?.Invoke();
        }
    }
    [SerializeField] int m_chillExperience = 0;
    [SerializeField] Sound chillLevelUpSound;
    public Action onChillExperienceChange;
    public Action<int> onChillExperienceEarn;
    public int chillExperience
    {
        get => m_chillExperience;
        set
        {
            if (value > m_chillExperience) onChillExperienceEarn?.Invoke(value - m_chillExperience);
            m_chillExperience = value;
            onChillExperienceChange?.Invoke();
            if(m_chillExperience >= chillExperienceRequired)
            {
                chillExperience -= chillExperienceRequired;
                chillLevel++;
                AudioManager.Instance.PlaySound(chillLevelUpSound);
            }
        }
    }
    public int chillExperienceRequired => 500 + 100 * chillLevel;

    public void Save(SaveData data)
    {
        data.player.integers["money"] = money;
        data.player.integers["chillLevel"] = chillLevel;
        data.player.integers["chillExperience"] = chillExperience;
    }

    public void Load(SaveData data)
    {
        if (data.player.integers.TryGetValue("money", out int tmp)) money = tmp;
        if (data.player.integers.TryGetValue("chillLevel", out int tmp2)) chillLevel = tmp2;
        if(data.player.integers.TryGetValue("chillExperience", out int tmp3)) chillExperience = tmp3;
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(Player_Statistics))]
public class Player_Statistics_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Give 100 Experience")) (target as Player_Statistics).chillExperience += 100;
    }
}
#endif
