using System;
using System.Collections.Generic;
using UnityEngine;
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
        throw new NotImplementedException();
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
        Debug.Log($"Item merge data created for item: {_item.name}. Total items: {m_itemMergeDataDictionary.Count}");
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
