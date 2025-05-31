using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static Action<Item> itemClicked;

    [Header(" Settings ")]
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
            m_currentItem = null;
            return;
        }

        if(!hitInfo.collider.TryGetComponent(out Item item))
        {
            m_currentItem = null;
            return;
        }

        Debug.Log($"Hit: {hitInfo.collider.name} at {hitInfo.point}");

        m_currentItem = item;
        
    }
    private void HandleMouseUp()
    {
        if(m_currentItem == null)
        {
            return;
        }

        itemClicked?.Invoke(m_currentItem);
        m_currentItem = null;
    }
}
