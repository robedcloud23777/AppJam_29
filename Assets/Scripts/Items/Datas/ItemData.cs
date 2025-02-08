using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemData : ScriptableObject
{
    [Header("Item")]
    [SerializeField] LangText m_itemName;
    [SerializeField] LangText m_description;
    [SerializeField] Sprite m_itemIcon;
    [SerializeField] ItemCategory m_category;
    public LangText itemName => m_itemName;
    public LangText description => m_description;
    public Sprite itemIcon => m_itemIcon;
    public ItemCategory category => m_category;
    public abstract Item Create();
    public abstract int maxStack { get; }
    public virtual string unit => "x";
}
public abstract class Item
{
    public readonly ItemData data;
    public virtual bool canDrop => true;
    public virtual LangText GetName() => data.itemName;
    public virtual LangText GetDescription() => data.description;
    public Item(ItemData data)
    {
        this.data = data;
        ItemManager.Instance.AddItem(this);
    }
    public virtual bool IsStackable(Item other) => other != null && data == other.data;
    public virtual Item Copy() => data.Create();

    public InventorySlot containedSlot = null;
    public Player wielder { get; private set; }


    public Action<Player> onWield, onUnwield;
    public Action onWieldUpdate;
    public Action onUpdate;
    public virtual void Update()
    {
        onUpdate?.Invoke();
    }
    public virtual void OnWield(Player wielder)
    {
        this.wielder = wielder;
        onWield?.Invoke(wielder);
    }
    public virtual void OnWieldUpdate()
    {
        onWieldUpdate?.Invoke();
    }
    public virtual void OnUnwield(Player wielder)
    {
        this.wielder = null;
        onUnwield?.Invoke(wielder);
    }
    public virtual void Delete()
    {
        ItemManager.Instance.RemoveItem(this);
    }
    public virtual void Save(DataUnit data) { }
    public virtual void Load(DataUnit data) { }
    public static LangText CategoryToLangText(ItemCategory category)
    {
        return new();
    }
}
[System.Serializable]
public struct ItemDataIntPair
{
    public ItemData data;
    public int count;
}
[System.Serializable]
public struct ItemIntPair
{
    public Item item;
    public int count;
}
[System.Serializable]
[Flags]
public enum ItemCategory
{

}