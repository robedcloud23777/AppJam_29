using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HpEffect : MonoBehaviour
{
    [SerializeField] HpComp origin;
    private void OnEnable()
    {
        origin.onDamage += OnDamage;
    }
    private void OnDisable()
    {
        origin.onDamage -= OnDamage;
    }
    void OnDamage(DamageReceivedData damage)
    {
        HpEffectText.Instantiate(origin.transform.position, damage);
    }
}