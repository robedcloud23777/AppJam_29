using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public UIManager() => Instance = this;
    [Header("Tabs")]
    [SerializeField] InventoryTab m_inventoryTab;
    [SerializeField] ShopTab m_shopTab;
    public InventoryTab inventoryTab => m_inventoryTab;
    public ShopTab shopTab => m_shopTab;

    [Header("Stats")]
    [SerializeField] TMP_Text moneyText;
    [SerializeField] TMP_Text levelText, experienceText;
    [SerializeField] Transform experienceBar;
    [SerializeField] FadeAwayText experienceGainText;
    [SerializeField] Transform experienceGainAnchor;
    [SerializeField] float experienceGainTextFadeTime = 1.0f;
    Tab openTab;

    Player_Statistics player;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Statistics>();
        player.onMoneyChange += OnMoneyChange;
        player.onChillExperienceChange += OnChillExperienceChange;
        player.onChillLevelChange += OnChillLevelChange;
        OnMoneyChange(); OnChillExperienceChange(); OnChillLevelChange();

        player.onChillExperienceEarn += OnChillExperienceEarn;
    }
    void OnMoneyChange()
    {
        moneyText.text = player.money.ToString();
    }
    void OnChillExperienceChange()
    {
        experienceText.text = $"{player.chillExperience}/{player.chillExperienceRequired}";
        experienceBar.DOKill();
        experienceBar.DOScaleX((float)player.chillExperience / player.chillExperienceRequired, 0.5f).SetEase(Ease.OutCirc);
    }
    void OnChillLevelChange()
    {
        levelText.text = $"Lv{player.chillLevel}";
    }
    void OnChillExperienceEarn(int amount)
    {
        FadeAwayText tmp = experienceGainText.Instantiate();
        tmp.transform.SetParent(experienceGainAnchor, false);
        tmp.transform.SetSiblingIndex(0);
        tmp.tmp_text.text = $"+{amount} Cxp";
        tmp.Set(experienceGainTextFadeTime);
    }
    public void OpenTab(Tab tab)
    {
        if(openTab == tab)
        {
            CloseTab(); return;
        }

        if (openTab != null) openTab.CloseTab();
        openTab = tab;
        if (openTab != null) openTab.OpenTab();
    }
    public void CloseTab()
    {
        if (openTab == null) return;
        openTab.CloseTab();
        openTab = null;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            OpenTab(inventoryTab);
        }
        if (Input.GetKeyDown(KeyCode.Escape)) CloseTab();
    }
}