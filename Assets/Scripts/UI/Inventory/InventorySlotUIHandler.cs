using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUIHandler : MonoBehaviour
{
    [SerializeField] InventorySlotUI grabbingSlotUI;

    [Header("Desc")]
    [SerializeField] RectTransform describer;
    [SerializeField] TMP_Text itemName, itemDesc;
    public InventorySlot grabbingSlot { get; } = new();
    private void Awake()
    {
        grabbingSlotUI.Set(grabbingSlot);
    }
    private void Update()
    {
        GrabUpdate();
        DescUpdate();
    }
    void GrabUpdate()
    {
        if (grabbingSlot.item == null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                foreach (var i in UIScanner.ScanUI())
                {
                    if (i.gameObject.TryGetComponent(out InventorySlotUI slot) && slot.origin.item != null)
                    {
                        grabbingSlot.Swap(slot.origin);
                        break;
                    }
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                foreach (var i in UIScanner.ScanUI())
                {
                    if (i.gameObject.TryGetComponent(out InventorySlotUI slot) && slot.origin.item != null)
                    {
                        grabbingSlot.SetItem(slot.origin.item.Copy());
                        int count = slot.origin.count / 2 + slot.origin.count % 2;
                        grabbingSlot.count = count;
                        slot.origin.count -= count;
                        break;
                    }
                }
            }
        }
        else
        {
            grabbingSlotUI.transform.position = Input.mousePosition;
            if (Input.GetMouseButtonDown(0))
            {
                foreach (var i in UIScanner.ScanUI())
                {
                    if (i.gameObject.TryGetComponent(out InventorySlotUI slot))
                    {
                        grabbingSlot.Swap(slot.origin);
                        break;
                    }
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                foreach (var i in UIScanner.ScanUI())
                {
                    if (i.gameObject.TryGetComponent(out InventorySlotUI slot))
                    {
                        grabbingSlot.count -= 1 - slot.origin.Insert(grabbingSlot.item.Copy(), 1);
                    }
                }
            }
        }
    }
    void DescUpdate()
    {
        if(grabbingSlot.item == null)
        {
            bool found = false;
            foreach (var i in UIScanner.ScanUI())
            {
                if (i.gameObject.TryGetComponent(out InventorySlotUI slot) && slot.origin.item != null)
                {
                    found = true;
                    if (slot.origin.item != describing) Describe(slot.origin.item);
                    describer.pivot = new Vector2(Input.mousePosition.x > Screen.width / 2.0f ? 1.0f : 0.0f, Input.mousePosition.y > Screen.height / 2.0f ? 1.0f : 0.0f);
                    describer.position = Input.mousePosition;
                    describer.gameObject.SetActive(true);
                    break;
                }
            }
            if(!found) describer.gameObject.SetActive(false);
        }
        else
        {
            describer.gameObject.SetActive(false);
        }
    }
    Item describing = null;
    void Describe(Item item)
    {
        if (item == null) return;
        describing = item;
        itemName.text = item.data.itemName.text;
        itemDesc.text = item.data.description.text;
    }
}