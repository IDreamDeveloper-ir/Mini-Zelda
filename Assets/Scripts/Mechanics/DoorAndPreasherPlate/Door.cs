using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public List<ICondition> Conditions = new List<ICondition>();

    void IInteractable.OnInteract()
    {
        bool DoorShouldOpen = true;

        foreach (ICondition condition in Conditions)
        {
            if (!condition.Condition) { DoorShouldOpen = false; break; }
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
