using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliders : MonoBehaviour
{
    [SerializeField]
    private float interActionRange;
    [SerializeField]
    private bool interActionGizmos;
    [SerializeField]
    private LayerMask layer;

    private void Update()
    {
        InterAction();
    }

    private void InterAction()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, interActionRange, layer);
        
        if (colliders == null)
            return;

        if (colliders.Length < 1)
            return;

        IInterActionable target = colliders[0].GetComponent<IInterActionable>();
        if (Input.GetButtonDown("InterAction"))
        {
            target?.InterAction(this);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (interActionGizmos)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(transform.position, interActionRange);
        }
    }
}
