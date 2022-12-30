using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InterActionAdapter : MonoBehaviour, IInterActionable
{
    public UnityEvent<PlayerColliders> OnInterAction;

    public void InterAction(PlayerColliders collider)
    {
        OnInterAction?.Invoke(collider);
    }
}

