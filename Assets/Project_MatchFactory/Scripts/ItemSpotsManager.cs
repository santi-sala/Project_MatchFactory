using System;
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
        if (!FreeSpotAvailable())
        {
            Debug.LogError("No free spot available for the item! GAME OVER!");
            return;
        }

        HandleIemClicked(_item);

        // Turn the item as a child of the item spot
        //

        // Scale the item icon down, set its local position 0,0,0
        

        

    }

    private void HandleIemClicked(Item _item)
    {
        MoveItemToFirstFreeSpot(_item);
    }

    private void MoveItemToFirstFreeSpot(Item _item)
    {
        ItemSpot targetSpot = GetFreeSpot();

        if (targetSpot == null)
        {
            Debug.LogError("No free spot found for the item!");
            return;
        }

       targetSpot.Populate(_item);

        _item.transform.localPosition = m_itemLocalPositionOnSpot;
        _item.transform.localScale = m_itemLocalScaleOnSpot;
        _item.transform.localRotation = Quaternion.identity;

        
        _item.DisableShadows();

        
        _item.DisablePhysics();


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
