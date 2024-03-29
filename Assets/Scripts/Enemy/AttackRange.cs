using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    [HideInInspector]
    public CapsuleCollider attackRange;

    private Enemy enemy;

    private void Awake()
    {
        attackRange = GetComponent<CapsuleCollider>();
        enemy = GetComponentInParent<Enemy>();
    }
    private void Start()
    {
        attackRange.radius = enemy.data.attackRange;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            enemy.data.canAttack = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            enemy.data.canAttack = false;
        }    
    }
}
