using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ItemManager : MonoBehaviour, ISavable
{
    static ItemManager instance;
    public static ItemManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new GameObject().AddComponent<ItemManager>();
                DontDestroyOnLoad(instance);
            }
            return instance;
        }
    }
    List<Item> items = new(), addQueue = new(), removeQueue = new();
    public float itemRenderSpaceRotation = 0.0f;
    public void AddItem(Item item)
    {
        addQueue.Add(item);
    }
    public void RemoveItem(Item item)
    {
        removeQueue.Add(item);
    }
    const float itemRenderRotateSpeed = 90.0f;
    private void Update()
    {
        foreach(var i in items)
        {
            i.Update();
        }
        foreach(var i in addQueue)
        {
            items.Add(i);
        } addQueue.Clear();
        foreach(var i in removeQueue)
        {
            items.Remove(i);
        } removeQueue.Clear();
        itemRenderSpaceRotation += Time.deltaTime * itemRenderRotateSpeed;
        if (itemRenderSpaceRotation > 360.0f) itemRenderSpaceRotation -= 360.0f;
    }
    List<DroppedItem> droppedItems = new();
    public void AddDroppedItem(DroppedItem item)
    {
        droppedItems.Add(item);
    }
    public void RemoveDroppedItem(DroppedItem item)
    {
        droppedItems.Remove(item);
    }
    public void Save(SaveData data)
    {
        foreach(var i in droppedItems)
        {
            DataUnit tmp = new();
            tmp.strings["slotSave"] = JsonUtility.ToJson(i.slot.Save());
            tmp.floats["posX"] = i.transform.position.x;
            tmp.floats["posY"] = i.transform.position.y;
            data.droppedItems.Add(tmp);
        }
    }
    public void Load(SaveData data)
    {
        foreach(var i in data.droppedItems)
        {
            if(i.strings.TryGetValue("slotSave", out string tmp))
            {
                DroppedItem tmp2 = DroppedItem.Create();
                InventorySlotSaveData slotSave = JsonUtility.FromJson<InventorySlotSaveData>(tmp);
                tmp2.slot.Load(slotSave);

                Vector2 pos = Vector2.zero;
                if (i.floats.TryGetValue("posX", out float x)) pos.x = x;
                if (i.floats.TryGetValue("posY", out float y)) pos.y = y;
                tmp2.transform.position = pos;
            }
        }
    }
}