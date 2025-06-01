using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static Action<Item> itemClicked;

    [Header(" Settings ")]
    [SerializeField] private Material m_outlineMaterial;

    private Item m_currentItem;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            HandleDrag();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            HandleMouseUp();
        }
    }
    private void HandleDrag()
    {
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo, 100);

        if (hitInfo.collider == null)
        {
            DeselectCurrentItem();
            return;
        }

        if (hitInfo.collider.transform.parent == null)
        {
            return;
        }

        if (!hitInfo.collider.transform.parent.TryGetComponent(out Item item))
        {
            DeselectCurrentItem();
            return;
        }

        Debug.Log($"Hit: {hitInfo.collider.name} at {hitInfo.point}");

     
        DeselectCurrentItem();
        

        m_currentItem = item;
        m_currentItem.SelectItem(m_outlineMaterial);

    }

    private void DeselectCurrentItem()
    {
        if (m_currentItem != null)
        {
            m_currentItem.DeselectItem();
        }

        m_currentItem = null;
    }


    private void HandleMouseUp()
    {
        if(m_currentItem == null)
        {
            return;
        }

        m_currentItem.DeselectItem();

        itemClicked?.Invoke(m_currentItem);
        m_currentItem = null;
    }


}
