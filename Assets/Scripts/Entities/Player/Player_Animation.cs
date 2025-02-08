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
        origin.inventory.onEquippedItemChange += OnEquippedItemChange;
    }
    readonly int moveXID = Animator.StringToHash("MoveX");
    readonly int moveYID = Animator.StringToHash("MoveY");
    public void OnUpdate()
    {
        anim.SetFloat(moveXID, origin.movement.move.x);
        anim.SetFloat(moveYID, origin.movement.move.y);
    }
    readonly int animationTypeID = Animator.StringToHash("AnimationType");
    void OnEquippedItemChange(Item item)
    {
        anim.SetInteger(animationTypeID, (int)item.heldAnimation);
    }
}
public enum AnimationType
{
    None = 0
}