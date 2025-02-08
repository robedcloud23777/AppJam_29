using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ToolItemData : UnstackableItemData
{
    [Header("Tool")]
    [SerializeField] Tool m_prefab;
    public Tool prefab => m_prefab;
}
public abstract class ToolItem : Item
{
    new public readonly ToolItemData data;
    Tool m_tool;
    public Tool tool
    {
        get
        {
            if(m_tool == null)
            {
                m_tool = MonoBehaviour.Instantiate(data.prefab);
            }
            return m_tool;
        }
    }
    public ToolItem(ToolItemData data) : base(data)
    {
        this.data = data;
    }
    public override void OnWield(Player wielder)
    {
        base.OnWield(wielder);
        tool.transform.SetParent(wielder.inventory.equipmentAnchor, false);
        tool.transform.localPosition = Vector2.zero;
        tool.gameObject.SetActive(true);
    }
    public override void OnUnwield(Player wielder)
    {
        base.OnUnwield(wielder);
        tool.gameObject.SetActive(false);
    }
}