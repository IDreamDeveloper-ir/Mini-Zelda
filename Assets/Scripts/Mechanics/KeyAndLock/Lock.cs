using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour, ICondition, IInteractable
{
    [SerializeField] private Door Door;
    [SerializeField] private Item KeyItem;

    bool ICondition.Condition => Condition;
    private bool Condition;
    private void Awake()
    {
        Door.Conditions.Add(this);
    }

    void IInteractable.OnInteract()
    {
        Condition = InventoryManager.Instance.SearchForItem(KeyItem);
        if (Condition)
        {
            ((IInteractable)Door).OnInteract();
            InventoryManager.Instance.RemoveItem(KeyItem);
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
