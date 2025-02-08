using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player_Movement : MonoBehaviour
{
    Player origin;

    Rigidbody2D rb;
    [SerializeField] float moveSpeed;
    [SerializeField] Transform rotator;

    [Header("Chilling")]
    [SerializeField] float chillTimeRequired;
    [SerializeField] int chillExperienceGained;
    public Vector2 move { get; private set; }
    public float speedMultiplier = 1.0f;
    internal void OnAwake()
    {
        origin = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
    }
    float chillTime = 0.0f;
    internal void OnFixedUpdate()
    {
        move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (move.x < 0.0f) rotator.localScale = new Vector2(-1.0f, 1.0f);
        else if(move.x > 0.0f) rotator.localScale = new Vector2(1.0f, 1.0f);
        rb.linearVelocity = move * moveSpeed * speedMultiplier;

        if(move.magnitude < 0.01f)
        {
            chillTime += Time.fixedDeltaTime;
            if(chillTime >= chillTimeRequired)
            {
                chillTime = 0.0f;
                origin.statistics.chillExperience += chillExperienceGained;
            }
        }
        else
        {
            chillTime = 0.0f;
        }
    }
}
