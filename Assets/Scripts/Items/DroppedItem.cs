using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class DroppedItem : PooledPrefab<DroppedItem>
{
    public Rigidbody2D rb { get; private set; }
    public InventorySlot slot { get; } = new();
    static DroppedItem prefab;
    new public static DroppedItem Create()
    {
        if (prefab == null) prefab = Resources.Load<DroppedItem>("DroppedItem");
        return prefab.Instantiate();
    }

    [SerializeField] SpriteRenderer itemIcon;
    protected override void OnCreate()
    {
        base.OnCreate();
        rb = GetComponent<Rigidbody2D>();
        slot.onItemChange += OnItemChange;
    }
    void OnItemChange()
    {
        if (slot.item == null) Release();
        else itemIcon.sprite = slot.item.data.itemIcon;
    }
    protected override void OnRelease()
    {
        base.OnRelease();
        slot.count = 0;
    }
    const float despawnTime = 360.0f;
    float counter = 0.0f;
    private void OnEnable()
    {
        counter = 0.0f;
        ItemManager.Instance.AddDroppedItem(this);
    }
    private void Update()
    {
        counter += Time.deltaTime;
        if(counter >= despawnTime)
        {
            Release();
        }
    }
    private void OnDisable()
    {
        ItemManager.Instance.RemoveDroppedItem(this);
    }
}