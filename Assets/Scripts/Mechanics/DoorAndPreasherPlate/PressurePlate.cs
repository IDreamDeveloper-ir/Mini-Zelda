using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour, IDoorCondition
{
    [SerializeField] private Door Door;

    private void Awake()
    {
        Door.DoorConditions.Add(this);
    }

    bool IDoorCondition.DoorCondition => DoorCondition;
    private bool DoorCondition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PickHandler>())
        {
            DoorCondition = true;
            GetComponent<Animator>().Play("Activate");
            //((IInteractable)Door).OnInteract();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PickHandler>())
        {
            DoorCondition = false;
            GetComponent<Animator>().Play("Deactivate");
        }
    }
}
