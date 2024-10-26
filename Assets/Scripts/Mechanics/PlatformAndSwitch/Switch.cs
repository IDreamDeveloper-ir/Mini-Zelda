using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour,IInteractable
{
    [SerializeField] private Platform platform;

    void IInteractable.OnInteract()
    {
        platform.IsPlayerOnboard = !platform.IsPlayerOnboard;
        GetComponent<Animator>().speed = platform.IsPlayerOnboard ? 1 : - 1;
        GetComponent<Animator>().Play("SwitchOn");
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
