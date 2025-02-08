using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Cooldowns : MonoBehaviour
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
}
