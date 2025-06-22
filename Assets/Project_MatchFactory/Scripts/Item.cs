using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Item : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private EItemName m_itemName;
    public EItemName ItemName => m_itemName;
    private ItemSpot m_itemSpot;
    public ItemSpot ItemSpot => m_itemSpot;


    [Header("Elements")]
    [SerializeField] private Renderer m_renderer;
    [SerializeField] private Collider m_collider;
    private Material m_baseMaterial;


    private void Awake()
    {
        if (m_renderer == null)
        {
            m_renderer = GetComponent<Renderer>();
        }

        m_baseMaterial = m_renderer.material;
    }

    public void AssignSpot(ItemSpot _spot) 
    { 
        this.m_itemSpot = _spot;
    } 
    internal void DisablePhysics()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        m_collider.enabled = false;
    }

    internal void DisableShadows()
    {
        m_renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
    }

    public void SelectItem(Material _outlineMaterial) 
    { 
        m_renderer.materials = new Material[2] { m_baseMaterial, _outlineMaterial };
    }

    public void DeselectItem()
    {
        m_renderer.materials = new Material[1] { m_baseMaterial };
    }
}
