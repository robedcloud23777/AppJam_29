using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryBeltUI : MonoBehaviour
{
    [SerializeField] Transform slotIndicator;
    [SerializeField] InventorySlotUI slotPrefab;
    [SerializeField] Transform slotAnchor;
    List<InventorySlotUI> slots = new();

    Player_Inventory player;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Inventory>();
        for (int i = 0; i < Player_Inventory.beltLength; i++)
        {
            InventorySlotUI tmp = Instantiate(slotPrefab, slotAnchor);
            tmp.Set(player.slots[i]);
            slots.Add(tmp);
        }
    }
    private void Update()
    {
        slotIndicator.transform.position = slots[player.equippedSlotIndex].transform.position;
    }
}