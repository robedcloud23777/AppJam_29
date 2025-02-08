using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HpEffectText : PooledPrefab<HpEffectText>
{
    [SerializeField] TMP_Text text;
    [SerializeField] float disappearSpread = 100.0f;
    [SerializeField] float disappearTime = 1.0f;
    static HpEffectText prefab;
    public static void Instantiate(Vector2 pos, DamageReceivedData damage)
    {
        if (prefab == null) prefab = Resources.Load<HpEffectText>("HpEffectText");
        HpEffectText tmp = prefab.Instantiate();
        tmp.Set(pos, damage);
    }
    protected override void OnCreate()
    {
        base.OnCreate();
        transform.SetParent(MainCanvas.Instance.transform);
        transform.localScale = Vector3.one;
    }
    void Set(Vector2 pos, DamageReceivedData damage)
    {
        float counter = 0.0f;

        float ang = UnityEngine.Random.Range(0.0f, 360.0f);
        Vector2 dir = new Vector2(Mathf.Cos(ang), Mathf.Sin(ang));
        text.text = damage.amount.ToString();
        transform.position = Camera.main.WorldToScreenPoint(pos);
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1.0f);
        DOTween.To(() => counter, value => counter = value, 1.0f, disappearTime).OnUpdate(() =>
        {
            transform.position = Camera.main.WorldToScreenPoint(pos) + (Vector3)dir * disappearSpread * counter;
            text.color = new Color(text.color.r, text.color.g, text.color.b, 1.0f - counter);
        }).OnComplete(() =>
        {
            Release();
        }).SetEase(Ease.OutCirc);
    }
}