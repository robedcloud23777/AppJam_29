using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Cooldowns : MonoBehaviour, ISavable
{
    List<CooldownSource> cooldowns = new();
    public void AddCooldown(CooldownSource source)
    {
        if (cooldowns.Contains(source)) return;
        cooldowns.Add(source);
    }
    List<CooldownSource> removeQueue = new();
    internal void OnUpdate()
    {
        foreach(var i in cooldowns)
        {
            i.cooldownLeft = Mathf.Max(0.0f, i.cooldownLeft - Time.deltaTime);
            if (i.cooldownLeft <= 0) removeQueue.Add(i);
        }
        foreach (var i in removeQueue) cooldowns.Remove(i); removeQueue.Clear();
    }

    public void Save(SaveData data)
    {
        CooldownsSave tmp = new();
        foreach(var i in cooldowns)
        {
            tmp.cooldowns.Add(new()
            {
                source = i,
                cooldown = i.cooldown,
                cooldownLeft = i.cooldownLeft
            });
        }
        data.player.strings["cooldowns"] = JsonUtility.ToJson(tmp);
    }

    public void Load(SaveData data)
    {
        if(data.player.strings.TryGetValue("cooldowns", out string tmp))
        {
            CooldownsSave tmp2 = JsonUtility.FromJson<CooldownsSave>(tmp);
            foreach(var i in tmp2.cooldowns)
            {
                i.source.cooldown = i.cooldown;
                i.source.cooldownLeft = i.cooldownLeft;
                cooldowns.Add(i.source);
            }
        }
    }
}
[System.Serializable]
public class CooldownsSave
{
    public List<CooldownFloatPair> cooldowns = new();
}
[System.Serializable]
public class CooldownFloatPair
{
    public CooldownSource source;
    public float cooldown, cooldownLeft;
}