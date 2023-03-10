using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class PlayerColliders : MonoBehaviour, IDamageable
{
    [SerializeField]
    private float interActionRange;
    [SerializeField]
    private bool interActionGizmos;
    [SerializeField]
    private LayerMask layer;

    private bool canKnockBack = true;

    private Animator anim;
    private PlayerController playerCon;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        playerCon = GetComponent<PlayerController>();
    }

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

    public void TakeDamage(int damage)
    {
        if (!playerCon.invincibility)
        {
            if (canKnockBack)
            {
                anim.SetTrigger("Hit");
                canKnockBack = false;
                StartCoroutine(KnockBackPossible());
            }
            PlayerStatManager.Instance.CalculateTakeDamage(damage);
        }
    }

    public IEnumerator KnockBackPossible()
    {
        yield return new WaitForSeconds(3);
        canKnockBack = true;
    }
}
