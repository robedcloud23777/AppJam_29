using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player_Movement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float moveSpeed;
    public Vector2 move { get; private set; }
    internal void OnAwake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    internal void OnFixedUpdate()
    {
        move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rb.linearVelocity = move * moveSpeed;
    }
}
