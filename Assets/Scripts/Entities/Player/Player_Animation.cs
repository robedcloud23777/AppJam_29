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
    readonly int movingID = Animator.StringToHash("Moving");
    public void OnUpdate()
    {
        anim.SetBool(movingID, origin.movement.move.magnitude > 0.1f);
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