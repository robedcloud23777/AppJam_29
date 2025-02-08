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

    public InventorySlot origin { get; private set; }
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
        if (origin.item == null) itemIcon.gameObject.SetActive(false);
        else
        {
            itemIcon.sprite = origin.item.data.itemIcon;
            itemIcon.gameObject.SetActive(true);
        }
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
}