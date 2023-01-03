using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    [HideInInspector]
    public CapsuleCollider attackRange;

    public bool canAttack = false;

    private void Awake()
    {
        attackRange = GetComponent<CapsuleCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        canAttack = true;
    }

    private void OnTriggerExit(Collider other)
    {
        canAttack = false;
    }
}
