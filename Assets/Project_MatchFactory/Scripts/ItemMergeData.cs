using UnityEngine;
using System.Collections.Generic;

public struct ItemMergeData
{
    private string m_itemName;
    public string ItemName => m_itemName;

    private List<Item> m_items;
    public List<Item> Items => m_items;

    public ItemMergeData(Item _firstItem)
    {
        m_itemName = _firstItem.name;
        m_items = new List<Item>();
        m_items.Add(_firstItem);
    }
}
