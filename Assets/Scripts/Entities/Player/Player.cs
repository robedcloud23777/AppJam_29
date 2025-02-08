using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player_Movement), typeof(Player_Animation), typeof(Player_Inventory))]
[RequireComponent(typeof(Player_Cooldowns))]
public class Player : MonoBehaviour
{
    internal Player_Movement movement { get; private set; }
    new internal Player_Animation animation { get; private set; }
    internal Player_Inventory inventory { get; private set; }
    internal Player_Cooldowns cooldowns { get; private set; }
    private void Awake()
    {
        movement = GetComponent<Player_Movement>();
        animation = GetComponent<Player_Animation>();
        inventory = GetComponent<Player_Inventory>();
        cooldowns = GetComponent<Player_Cooldowns>();
        movement.OnAwake();
        animation.OnAwake();
        inventory.OnAwake();
    }
    private void Update()
    {
        cooldowns.OnUpdate();
        animation.OnUpdate();
        inventory.OnUpdate();
    }
    private void FixedUpdate()
    {
        movement.OnFixedUpdate();
    }
}
