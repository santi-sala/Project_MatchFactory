using UnityEngine;
using System.Collections.Generic;
using System;

public struct ItemMergeData
{
    private EItemName m_itemName;
    public EItemName ItemName => m_itemName;

    private List<Item> m_items;
    public List<Item> Items => m_items;

    public ItemMergeData(Item _firstItem)
    {
        m_itemName = _firstItem.ItemName;
        m_items = new List<Item>();
        m_items.Add(_firstItem);
    }

    public void AddItem(Item _item)
    {
        m_items.Add(_item);
    }

    internal bool CanMergeItems()
    {
        return m_items.Count >= 3;
    }
}
