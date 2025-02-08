using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Inventory : Inventory, ISavable
{
    Player origin;
    [SerializeField] Transform m_equipmentAnchor;
    [SerializeField] ItemDataIntPair[] startContent;
    [SerializeField] ItemObtainText obtainTextPrefab;
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

    public const int beltLength = 6;
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
    protected override void OnInsert(Item item, int count)
    {
        base.OnInsert(item, count);
        if (count == 0) return;
        obtainTextPrefab.Instantiate().Set(item, count, transform.position);
    }
    public void Insert_DropRest(Item item, int count)
    {
        count = Insert(item, count);
        if(count > 0)
        {
            DropItem(item.Copy(), count);
        }
    }
    public void DropItem(Item item, int count)
    {

    }

    public void Save(SaveData data)
    {
        data.player.strings["inventory"] = JsonUtility.ToJson(Save());
    }

    public void Load(SaveData data)
    {
        if (data.player.strings.TryGetValue("inventory", out string tmp)) Load(JsonUtility.FromJson<InventorySaveData>(tmp));
    }
}