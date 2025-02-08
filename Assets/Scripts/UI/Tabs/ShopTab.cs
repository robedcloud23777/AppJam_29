using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using NUnit.Framework.Constraints;
using static UnityEditor.Progress;

public class ShopTab : Tab
{
    [Header("Tab")]
    [SerializeField] float toggleTime = 0.25f;
    [SerializeField] float openPivotY, closedPivotY;
    [SerializeField] new RectTransform transform;
    Tween tween;

    [Header("ShopTab")]
    [SerializeField] InventorySlotUI sellSlotUI;

    [SerializeField] BuyListElement buyElementPrefab;
    [SerializeField] Transform buyElementAnchor;
    List<BuyListElement> buyElements = new();

    [SerializeField] SellListElement sellElementPrefab;
    [SerializeField] Transform sellElementAnchor;
    [SerializeField] TMP_Text sellPriceText;
    [SerializeField] Button sellButton;
    List<SellListElement> sellElements = new();

    InventorySlot sellSlot = new();

    Shop currentShop;
    Player player;
    private void OnEnable()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            sellSlot.slotRestriction = SellSlotRestriction;
            sellSlot.onCountChange += UpdateSellPrice;
            sellSlot.onItemChange += UpdateSellPrice;
            sellSlotUI.Set(sellSlot);
            sellButton.onClick.AddListener(Sell);
        }
    }
    bool SellSlotRestriction(Item item)
    {
        if (currentShop == null) return false;
        if (item == null) return true;
        foreach(var i in currentShop.sell)
        {
            if (i.item == item.data) return true;
        }
        return false;
    }
    void Sell()
    {
        if (currentShop == null) return;
        foreach (var i in currentShop.sell)
        {
            if (i.item == sellSlot.item.data) player.statistics.money += i.buyingFor * sellSlot.count;
        }
        sellSlot.count = 0;
    }
    void UpdateSellPrice()
    {
        int tmp = 0;
        if(sellSlot.item != null)
        {
            foreach (var i in currentShop.sell)
            {
                if (i.item == sellSlot.item.data) tmp += i.buyingFor * sellSlot.count;
            }
        }
        sellPriceText.text = $"{tmp}$";
    }
    public void Set(Shop shop)
    {
        currentShop = shop;
        int i;
        for(i = 0; i < shop.sell.Length; i++)
        {
            if (sellElements.Count <= i) sellElements.Add(Instantiate(sellElementPrefab, sellElementAnchor));
            sellElements[i].gameObject.SetActive(true);
            sellElements[i].Set(shop.sell[i]);
        }
        for(; i < sellElements.Count; i++)
        {
            sellElements[i].gameObject.SetActive(false);
        }
        for(i = 0; i < shop.buy.Length; i++)
        {
            if (buyElements.Count <= i) buyElements.Add(Instantiate(buyElementPrefab, buyElementAnchor));
            buyElements[i].gameObject.SetActive(true);
            buyElements[i].Set(shop.buy[i]);
        }
        for(; i < buyElements.Count; i++)
        {
            buyElements[i].gameObject.SetActive(false);
        }
    }
    public override void CloseTab()
    {
        if(sellSlot.item != null)
        {
            player.inventory.Insert_DropRest(sellSlot.item, sellSlot.count);
            sellSlot.count = 0;
        }
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