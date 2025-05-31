using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(SphereCollider))]
public class Item : MonoBehaviour
{
    internal void DisablePhysics()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<SphereCollider>().enabled = false;
    }

    internal void DisableShadows()
    {
    }
}
