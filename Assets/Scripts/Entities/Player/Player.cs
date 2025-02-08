using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player_Movement), typeof(Player_Animation), typeof(Player_Inventory))]
public class Player : MonoBehaviour
{
    internal Player_Movement movement { get; private set; }
    new internal Player_Animation animation { get; private set; }
    internal Player_Inventory inventory { get; private set; }
    private void Awake()
    {
        movement = GetComponent<Player_Movement>();
        animation = GetComponent<Player_Animation>();
        inventory = GetComponent<Player_Inventory>();
        movement.OnAwake();
        animation.OnAwake();
        inventory.OnAwake();
    }
    private void Update()
    {
        animation.OnUpdate();
    }
    private void FixedUpdate()
    {
        movement.OnFixedUpdate();
    }
}
