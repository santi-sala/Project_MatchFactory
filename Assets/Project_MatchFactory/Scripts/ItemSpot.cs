using UnityEngine;

public class ItemSpot : MonoBehaviour
{
    [Header(" Settings ")]
    private Item m_item;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Populate(Item _item) 
    {
        m_item = _item;
        _item.transform.SetParent(transform);
    }
    
    public bool IsEmpty() 
    { 
        return m_item == null;
    }
}
