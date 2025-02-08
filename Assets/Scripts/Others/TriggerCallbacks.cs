using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class TriggerCallbacks : MonoBehaviour
{
    public Action<Collider2D> onTriggerEnter, onTriggerStay, onTriggerExit;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        onTriggerEnter?.Invoke(collision);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        onTriggerStay?.Invoke(collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        onTriggerExit?.Invoke(collision);
    }
}