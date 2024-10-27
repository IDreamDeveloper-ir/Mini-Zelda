using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour, IInteractable
{
    [SerializeField] private Item Item;
    void IInteractable.OnInteract()
    {
       bool result = InventoryManager.Instance.AddItem(Item);
        //Debug.Log(name + ": " + result);
        if (result)
        {
            InteractionManager.Instance.ClearMe(this);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<InteractionManager>().SetObjectToInteract(this);
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
