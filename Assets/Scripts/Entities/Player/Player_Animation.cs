using System;
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
        AddAttackCallbacks();
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
        if(item == null) anim.SetInteger(animationTypeID, (int)AnimationType.None);
        else anim.SetInteger(animationTypeID, (int)item.heldAnimation);
    }
    readonly int attackID = Animator.StringToHash("Attack");
    public void TriggerAttack()
    {
        anim.SetTrigger(attackID);
    }
    public Action onAttackStart, onAttackEnd;
    void AddAttackCallbacks()
    {
        foreach (var i in anim.GetBehaviours<AttackStartBehaviour>()) i.onAttackStart += () => { onAttackStart?.Invoke(); };
        foreach (var i in anim.GetBehaviours<AttackEndBehaviour>()) i.onAttackEnd += () => { onAttackEnd?.Invoke(); };
    }
}
public enum AnimationType
{
    None = 0
}