using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InventoryTab : Tab
{
    [SerializeField] InventorySlotUI slotPrefab;
    [SerializeField] Transform slotAnchor;
    List<InventorySlotUI> slots = new();

    [SerializeField] float toggleTime = 0.25f;
    [SerializeField] float openPivotY, closedPivotY;
    [SerializeField] new RectTransform transform;
    Tween tween;

    Player_Inventory player;
    private void OnEnable()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Inventory>();
            player.onSlotCountChange += OnSlotCountChange;
            OnSlotCountChange();
        }
    }
    void OnSlotCountChange()
    {
        int i;
        for(i = 0; i < player.slotCount - Player_Inventory.beltLength; i++)
        {
            if(slots.Count <= i)
            {
                InventorySlotUI slot = Instantiate(slotPrefab, slotAnchor);
                slot.Set(player.slots[i + Player_Inventory.beltLength]);
                slots.Add(slot);
            }
            slots[i].gameObject.SetActive(true);
        }
        for(; i < slots.Count; i++)
        {
            slots[i].gameObject.SetActive(false);
        }
    }
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