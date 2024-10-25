using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance;
    public Transform HoldLocation;
    [NonSerialized] public IInteractable PickedObj;

    private void Awake()
    {
        Instance = this;
    }

    private List<IInteractable> Interactables = new List<IInteractable>();

    public void SetObjectToInteract(IInteractable obj)
    {
        if (!Interactables.Contains(obj)) Interactables.Add(obj);
    }

    public void ClearMe(IInteractable obj)
    {
        Interactables.Remove(obj);
    }

    public void Interact()
    {
       if (Interactables.Count > 0) Interactables[Interactables.Count - 1 ]?.OnInteract();
    }
}
