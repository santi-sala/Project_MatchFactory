using System;
using UnityEngine;

public class ItemSpotsManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Transform m_itemSpot;

    [Header(" Settings ")]
    [SerializeField] private Vector3 m_itemLocalPositionOnSpot;
    [SerializeField] private Vector3 m_itemLocalScaleOnSpot;


    private void Awake()
    {
        InputManager.itemClicked += OnItemClicked;
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
    private void OnItemClicked(Item item)
    {
        // Turn the item as a child of the item spot
        item.transform.SetParent(m_itemSpot);

        // Scale the item icon down, set its local position 0,0,0
        item.transform.localPosition = m_itemLocalPositionOnSpot;
        item.transform.localScale = m_itemLocalScaleOnSpot;

        // Disable the items shadow
        item.DisableShadows();

        // Disable the item collider and physics
        item.DisablePhysics();

    }
}
