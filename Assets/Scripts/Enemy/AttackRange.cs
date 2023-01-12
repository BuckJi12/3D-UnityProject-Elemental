using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    [HideInInspector]
    public CapsuleCollider attackRange;

    public bool canAttack = false;

    private Enemy enemy;

    private void Awake()
    {
        attackRange = GetComponent<CapsuleCollider>();
        enemy = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            enemy.canAttack = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            enemy.canAttack = false;
        }    
    }
}
