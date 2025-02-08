using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Player_Animation : MonoBehaviour
{
    Player origin;
    Animator anim;
    public void OnAwake()
    {
        origin = GetComponent<Player>();
        anim = GetComponent<Animator>();
    }
    readonly int moveXID = Animator.StringToHash("MoveX");
    readonly int moveYID = Animator.StringToHash("MoveY");
    public void OnUpdate()
    {
        anim.SetFloat(moveXID, origin.movement.move.x);
        anim.SetFloat(moveYID, origin.movement.move.y);
    }
}