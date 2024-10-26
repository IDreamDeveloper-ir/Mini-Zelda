using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickHandler : MonoBehaviour, IInteractable
{
    private bool _hasPickedUP = false;
    void IInteractable.OnInteract()
    {
        if((Object)InteractionManager.Instance.PickedObj == this)
        {
            if (_hasPickedUP)
            {
                _hasPickedUP = false;
                OnPickDown();
            }
            else
            {
                _hasPickedUP = true;
                OnPickUP(InteractionManager.Instance.HoldLocation);
            }
        }
    }

    void OnPickDown()
    {
        if(GetComponent<Rigidbody>() == null)
        {
            this.AddComponent<Rigidbody>().freezeRotation = true;
        }
        if (GetComponent<MeshCollider>() == null)
        {
            this.AddComponent<MeshCollider>().convex = true;
        }
        if (GetComponent<BoxCollider>() == null)
        {
            this.AddComponent<BoxCollider>().size = Vector3.one * 1.2f;
            GetComponent<BoxCollider>().isTrigger = true;
        }

        GetComponent<Rigidbody>().AddForce(transform.parent.forward * 50);

        transform.parent = null;

        InteractionManager.Instance.PickedObj = null;
    }

    void OnPickUP(Transform t)
    {
        if (GetComponent<Rigidbody>() != null)
        {
            Destroy(GetComponent<Rigidbody>());
        }
        if (GetComponent<MeshCollider>() != null)
        {
            Destroy(GetComponent<MeshCollider>());
        }
        if (GetComponent<BoxCollider>() != null)
        {
            Destroy(GetComponent<BoxCollider>());
        }

        transform.parent = t;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (InteractionManager.Instance.PickedObj == null)
            {
                other.GetComponent<InteractionManager>().SetObjectToInteract(this);
                InteractionManager.Instance.PickedObj = this;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<InteractionManager>().ClearMe(this);
            if ((Object)InteractionManager.Instance.PickedObj == this)
            {
                InteractionManager.Instance.PickedObj = null;
            }
            //Debug.Log(name);
        }
    }
}
