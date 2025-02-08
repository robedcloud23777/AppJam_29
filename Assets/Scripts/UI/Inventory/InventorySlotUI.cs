using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] Image itemIcon;
    [SerializeField] TMP_Text itemCount;

    [Header("Cooldowns")]
    [SerializeField] Transform cooldownScaler;

    public InventorySlot origin { get; private set; }
    Item displaying = null;
    public void Set(InventorySlot slot)
    {
        origin = slot;
        origin.onItemChange += OnItemChange;
        origin.onCountChange += OnCountChange;
        OnItemChange();
        OnCountChange();
    }
    void OnItemChange()
    {
        if(displaying != null)
        {

        }

        displaying = origin.item;

        if (displaying == null) itemIcon.gameObject.SetActive(false);
        else
        {
            itemIcon.sprite = displaying.data.itemIcon;
            itemIcon.gameObject.SetActive(true);
        }
        if(displaying is ICooldownDisplayed)
        {
            cooldownScaler.gameObject.SetActive(true);
            CooldownDisplayUpdate();
        }
        else cooldownScaler.gameObject.SetActive(false);
    }
    void OnCountChange()
    {
        if (origin.count <= 1) itemCount.gameObject.SetActive(false);
        else
        {
            itemCount.text = $"x{origin.count}";
            itemCount.gameObject.SetActive(true);
        }
    }
    private void Update()
    {
        if(displaying is ICooldownDisplayed)
        {
            CooldownDisplayUpdate();
        }
    }
    void CooldownDisplayUpdate()
    {
        ICooldownDisplayed tmp = displaying as ICooldownDisplayed;
        cooldownScaler.localScale = new Vector2(1.0f, tmp.cooldownLeft);
    }
}