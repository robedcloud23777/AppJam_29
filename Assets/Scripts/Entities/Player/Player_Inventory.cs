using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Inventory : Inventory
{
    Player origin;
    [SerializeField] Transform m_equipmentAnchor;
    [SerializeField] ItemDataIntPair[] startContent;
    public Transform equipmentAnchor => m_equipmentAnchor;
    internal void OnAwake()
    {
        origin = GetComponent<Player>();
        Init();

        for(int i = 0; i < startContent.Length; i++)
        {
            if (startContent[i].data == null) continue;
            slots[i].SetItem(startContent[i].data.Create());
            slots[i].count = startContent[i].count;
        }

        ChangeEquippedSlot(0);
    }
    internal void OnUpdate()
    {
        for(int i = 0; i < beltLength; i++)
        {
            if(Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                ChangeEquippedSlot(i);
                break;
            }
        }

        if (equipped != null) equipped.OnWieldUpdate();
    }
    public int equippedSlotIndex { get; private set; } = 1;
    InventorySlot equippedSlot;
    Item equipped;

    public const float beltLength = 6;
    void ChangeEquippedSlot(int index)
    {
        if (index < 0 || index >= beltLength || index == equippedSlotIndex) return;
        if (equippedSlot != null) equippedSlot.onItemChange -= OnEquippedSlotUpdate;
        equippedSlotIndex = index;
        equippedSlot = slots[index];
        equippedSlot.onItemChange += OnEquippedSlotUpdate;
        OnEquippedSlotUpdate();
    }
    void OnEquippedSlotUpdate()
    {
        ChangeEquippedItem(equippedSlot.item);
    }
    public Action<Item> onEquippedItemChange;
    void ChangeEquippedItem(Item item)
    {
        if (equipped != null) equipped.OnUnwield(origin);
        equipped = item;
        onEquippedItemChange?.Invoke(equipped);
        if (equipped != null) equipped.OnWield(origin);
    }
}