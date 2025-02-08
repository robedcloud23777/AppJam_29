using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InventoryTab : Tab
{
    [Header("Tab")]
    [SerializeField] float toggleTime = 0.25f;
    [SerializeField] float openPivotY, closedPivotY;
    [SerializeField] new RectTransform transform;
    Tween tween;

    public override void CloseTab()
    {
        if (tween != null) tween.Kill();
        tween = DOTween.To(() => transform.pivot.y, value => transform.pivot = new Vector2(transform.pivot.x, value), closedPivotY, toggleTime).OnUpdate(() =>
        {
            transform.anchoredPosition = Vector2.zero;
        }).OnComplete(() =>
        {
            gameObject.SetActive(false);
        }).SetEase(Ease.InCirc);
    }

    public override void OpenTab()
    {
        gameObject.SetActive(true);
        if (tween != null) tween.Kill();
        tween = DOTween.To(() => transform.pivot.y, value => transform.pivot = new Vector2(transform.pivot.x, value), openPivotY, toggleTime).OnUpdate(() =>
        {
            transform.anchoredPosition = Vector2.zero;
        }).SetEase(Ease.OutCirc);
    }
}