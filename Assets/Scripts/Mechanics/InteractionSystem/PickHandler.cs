using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickHandler : MonoBehaviour,IPickable
{
    void IPickable.OnPickDown()
    {
        if(GetComponent<Rigidbody>() == null)
        {
            this.AddComponent<Rigidbody>();
        }
        if (GetComponent<MeshCollider>() == null)
        {
            this.AddComponent<MeshCollider>().convex = true;
        }
        GetComponent<Rigidbody>().AddForce(transform.parent.forward * 50);

        transform.parent = null;
    }

    void IPickable.OnPickUP(Transform t)
    {
        if (GetComponent<Rigidbody>() != null)
        {
            Destroy(GetComponent<Rigidbody>());
        }
        if (GetComponent<MeshCollider>() != null)
        {
            Destroy(GetComponent<MeshCollider>());
        }

        transform.parent = t;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<InteractionManager>().SetObjectToPick(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<InteractionManager>().ClearMe(this);
        }
    }
}
