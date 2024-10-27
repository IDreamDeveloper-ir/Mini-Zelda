using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour, ICondition
{
    [SerializeField] private Door Door;

    private void Awake()
    {
        Door.Conditions.Add(this);
    }

    bool ICondition.Condition => Condition;
    private bool Condition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PickHandler>())
        {
            Condition = true;
            GetComponent<Animator>().Play("Activate");
            ((IInteractable)Door).OnInteract();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PickHandler>())
        {
            Condition = false;
            GetComponent<Animator>().Play("Deactivate");
        }
    }
}
