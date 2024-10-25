using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public List<IDoorCondition> DoorConditions = new List<IDoorCondition>();

    void IInteractable.OnInteract()
    {
        bool DoorShouldOpen = true;

        foreach (IDoorCondition condition in DoorConditions)
        {
            if (!condition.DoorCondition) { DoorShouldOpen = false; break; }
        }

        if (DoorShouldOpen)
        {
            GetComponent<Animator>().Play("DoorOpen");
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
