using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    [SerializeField] private Transform PickUPLocation;

    private IPickable pickable;
    private bool _ismyhandfull;

    public void SetObjectToPick(IPickable obj)
    {
        if (pickable == null) pickable = obj;
    }

    public void ClearMe(IPickable obj)
    {
        if (pickable == obj) pickable = null;
    }

    public void Pick()
    {
        if (pickable != null)
        {
            if (_ismyhandfull)
            {
                _ismyhandfull = false;
                pickable.OnPickDown();
            }
            else
            {
                _ismyhandfull = true;
                pickable.OnPickUP(PickUPLocation);
            }
        }
    }
}
