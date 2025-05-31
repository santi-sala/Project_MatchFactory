using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleMouseDown();
        }
        
    }

    private void HandleMouseDown()
    {
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo, 100);

        if (hitInfo.collider == null)
        {
            return;
        }

        Debug.Log($"Hit: {hitInfo.collider.name} at {hitInfo.point}");
    }
}
