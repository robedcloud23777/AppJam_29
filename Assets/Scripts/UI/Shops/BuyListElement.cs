using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BuyListElement : MonoBehaviour
{
    [SerializeField] Image itemIcon;
    [SerializeField] TMP_Text count, price;

    [SerializeField] GameObject locked;
    [SerializeField] TMP_Text requiredLevel;

    [SerializeField] Button buyButton;
    Player m_player;
    Player player
    {
        get
        {
            if (m_player == null) m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            return m_player;
        }
    }
    private void OnEnable()
    {
        player.statistics.onChillLevelChange += OnChillLevelChange;
        buyButton.onClick.AddListener(OnBuyAttempt);
        OnChillLevelChange();
    }
    private void OnDisable()
    {
        player.statistics.onChillLevelChange -= OnChillLevelChange;
        buyButton.onClick.RemoveListener(OnBuyAttempt);
    }
    void OnChillLevelChange()
    {
        if (setElement == null) return;
        Debug.Log(locked);
        Debug.Log(setElement == null);
        locked.SetActive(player.statistics.chillLevel < setElement.requiredLevel);
    }
    BuyFromShopElement setElement;
    public void Set(BuyFromShopElement element)
    {
        setElement = element;
        itemIcon.sprite = element.item.itemIcon;
        count.text = $"x{element.count}";
        price.text = $"{element.sellingFor}$";
        requiredLevel.text = $"Lv{element.requiredLevel}";

        OnChillLevelChange();
    }
    void OnBuyAttempt()
    {
        if (setElement == null) return;
        if (player.statistics.chillLevel < setElement.requiredLevel || player.statistics.money < setElement.sellingFor) return;
        player.statistics.money -= setElement.sellingFor;
        player.inventory.Insert_DropRest(setElement.item.Create(), setElement.count);
    }
}