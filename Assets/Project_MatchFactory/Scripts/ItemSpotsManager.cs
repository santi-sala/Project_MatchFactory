using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;
public class ItemSpotsManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Transform m_itemSpotParent;
    private ItemSpot[] m_itemSpots;

    [Header(" Settings ")]
    [SerializeField] private Vector3 m_itemLocalPositionOnSpot;
    [SerializeField] private Vector3 m_itemLocalScaleOnSpot;
    private bool m_isBusy;

    [Header(" Data ")]
    private Dictionary<EItemName, ItemMergeData> m_itemMergeDataDictionary = new Dictionary<EItemName, ItemMergeData>();


    private void Awake()
    {
        InputManager.itemClicked += OnItemClicked;
        StoreSpots();
    }

    private void OnDestroy()
    {
        InputManager.itemClicked -= OnItemClicked;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnItemClicked(Item _item)
    {
        if (m_isBusy)
        {
            Debug.LogWarning("ItemSpotsManager is busy, ignoring item click.");
            return;
        }

        if (!FreeSpotAvailable())
        {
            Debug.LogError("No free spot available for the item! GAME OVER!");
            return;
        }

        m_isBusy = true;

        HandleItemClicked(_item);

        // Turn the item as a child of the item spot
        //

        // Scale the item icon down, set its local position 0,0,0
        

        

    }

    private void HandleItemClicked(Item _item)
    {
        if(m_itemMergeDataDictionary.ContainsKey(_item.ItemName))
        {
            HandleItemMergeDataFound(_item);
        }
        else
        {
            MoveItemToFirstFreeSpot(_item);
        }
    }

    private void HandleItemMergeDataFound(Item _item)
    {
        ItemSpot idealSpot = GetIdealSpot(_item);

        m_itemMergeDataDictionary[_item.ItemName].AddItem(_item);

        TryMoveItemToIdealSpot(_item, idealSpot);
    }

    private ItemSpot GetIdealSpot(Item _item)
    {
        List<Item> items = m_itemMergeDataDictionary[_item.ItemName].Items;
        List<ItemSpot> itemSpots = new List<ItemSpot>();

        for (int i = 0; i < items.Count; i++)
        {
            itemSpots.Add(items[i].ItemSpot);
        }

        if(itemSpots.Count >= 2) 
        {
            itemSpots.Sort((a, b) => b.transform.GetSiblingIndex().CompareTo(a.transform.GetSiblingIndex()));
        }

        int idealSpotIndex = itemSpots[0].transform.GetSiblingIndex() + 1;

        return m_itemSpots[idealSpotIndex];
    }

    private void TryMoveItemToIdealSpot(Item _item, ItemSpot _idealSpot)
    {
        if (!_idealSpot.IsEmpty()) 
        { 
            HandleIdealSpotFull(_item, _idealSpot);
            return;
        }

        MoveItemToSpot(_item, _idealSpot);
    }

    private void HandleIdealSpotFull(Item item, ItemSpot idealSpot)
    {
        throw new NotImplementedException();
    }

    private void MoveItemToSpot(Item _item, ItemSpot _targetSpot)
    {
        _targetSpot.Populate(_item);

        _item.transform.localPosition = m_itemLocalPositionOnSpot;
        _item.transform.localScale = m_itemLocalScaleOnSpot;
        _item.transform.localRotation = Quaternion.identity;

        _item.DisableShadows();
        _item.DisablePhysics();

        HandleItemReachedSpot(_item);
    }

    private void HandleItemReachedSpot(Item _item)
    {
        if (m_itemMergeDataDictionary[_item.ItemName].CanMergeItems()) 
        {
            MergeItems(m_itemMergeDataDictionary[_item.ItemName]);
        }
        else
        {
            CheckForGameOver();
        }
    }

    private void MergeItems(ItemMergeData _itemMergeData)
    {
        List<Item> items = _itemMergeData.Items;

        m_itemMergeDataDictionary.Remove(_itemMergeData.ItemName);

        for (int i = 0; i < items.Count; i++)
        {
            items[i].ItemSpot.ClearSpot();
            Destroy(items[i].gameObject);
        }

        m_isBusy = false;

    }

    private void MoveItemToFirstFreeSpot(Item _item)
    {
        ItemSpot targetSpot = GetFreeSpot();

        if (targetSpot == null)
        {
            Debug.LogError("No free spot found for the item!");
            return;
        }

        CreateItemMergeData(_item);

        targetSpot.Populate(_item);

        _item.transform.localPosition = m_itemLocalPositionOnSpot;
        _item.transform.localScale = m_itemLocalScaleOnSpot;
        _item.transform.localRotation = Quaternion.identity;

        
        _item.DisableShadows();

        
        _item.DisablePhysics();

        HandleFirstItemReachSpot(_item);
    }

    private void HandleFirstItemReachSpot(Item _item)
    {
        CheckForGameOver();
    }

    private void CheckForGameOver()
    {
        if(GetFreeSpot() == null) 
        {
            Debug.LogError("Game Over!!");
        }
        else
        {
            m_isBusy = false;
        }
    }

    private void CreateItemMergeData(Item _item)
    {
        m_itemMergeDataDictionary.Add(_item.ItemName, new ItemMergeData(_item));
    }

    private ItemSpot GetFreeSpot()
    {
        for (int i = 0; i < m_itemSpots.Length; i++)
        {
            if (m_itemSpots[i].IsEmpty())
            {
                return m_itemSpots[i];
            }
        }

        return null;
    }

    private bool FreeSpotAvailable()
    {
        for (int i = 0; i < m_itemSpots.Length; i++)
        {
            if (m_itemSpots[i].IsEmpty())
            {
                return true;
            }
        }
            return false;
    }

    private void StoreSpots()
    {
        m_itemSpots = new ItemSpot[m_itemSpotParent.childCount];

        for (int i = 0; i < m_itemSpotParent.childCount; i++)
        {
            m_itemSpots[i] = m_itemSpotParent.GetChild(i).GetComponent<ItemSpot>();
        }
    }
}
