using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player_Movement), typeof(Player_Animation))]
public class Player : MonoBehaviour
{
    internal Player_Movement movement { get; private set; }
    new internal Player_Animation animation { get; private set; }
    private void Awake()
    {
        movement = GetComponent<Player_Movement>();
        animation = GetComponent<Player_Animation>();
        movement.OnAwake();
        animation.OnAwake();
    }
    private void Update()
    {
        
    }
    private void FixedUpdate()
    {
        movement.OnFixedUpdate();
    }
}
