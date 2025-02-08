using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotListUI : MonoBehaviour
{
    [SerializeField] InventorySlotUI slotPrefab;
    [SerializeField] Transform slotAnchor;
    List<InventorySlotUI> slots = new();

    Player_Inventory player;
    private void OnEnable()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Inventory>();
            player.onSlotCountChange += OnSlotCountChange;
            OnSlotCountChange();
        }
    }
    void OnSlotCountChange()
    {
        int i;
        for (i = 0; i < player.slotCount - Player_Inventory.beltLength; i++)
        {
            if (slots.Count <= i)
            {
                InventorySlotUI slot = Instantiate(slotPrefab, slotAnchor);
                slot.Set(player.slots[i + Player_Inventory.beltLength]);
                slots.Add(slot);
            }
            slots[i].gameObject.SetActive(true);
        }
        for (; i < slots.Count; i++)
        {
            slots[i].gameObject.SetActive(false);
        }
    }
}