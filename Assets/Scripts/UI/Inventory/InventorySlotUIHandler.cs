using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUIHandler : MonoBehaviour
{
    [SerializeField] InventorySlotUI grabbingSlotUI;
    public InventorySlot grabbingSlot { get; } = new();
    private void Awake()
    {
        grabbingSlotUI.Set(grabbingSlot);
    }
    private void Update()
    {
        if(grabbingSlot.item == null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                foreach(var i in UIScanner.ScanUI())
                {
                    if(i.gameObject.TryGetComponent(out InventorySlotUI slot) && slot.origin.item != null)
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
                foreach(var i in UIScanner.ScanUI())
                {
                    if(i.gameObject.TryGetComponent(out InventorySlotUI slot))
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
}