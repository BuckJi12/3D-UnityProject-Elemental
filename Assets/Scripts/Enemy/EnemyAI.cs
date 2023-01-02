using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private Detect detect;
    private AttackRange attackRange;
    private Animator anim;

    private GameObject target;

    private EnemyState enemyState;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        detect = GetComponentInChildren<Detect>();
        attackRange = GetComponentInChildren<AttackRange>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        enemyState = EnemyState.Normal;
    }

    private void Update()
    {
        switch (enemyState)
        {
            case EnemyState.Normal:
                break;
            case EnemyState.Combat:
                Chase();
                Attack();
                break;
        }
    }

    public void SwitchMode()
    {
        if (enemyState == EnemyState.Normal)
        {
            enemyState = EnemyState.Combat;
            anim.SetTrigger("Move");
        }
        else if (enemyState == EnemyState.Combat)
        {
            enemyState = EnemyState.Normal;
            anim.SetTrigger("Idle");
        }
    }

    public void Attack()
    {
        if (attackRange.canAttack)
        {
            anim.SetTrigger("Attack");
        }
    }
    public void Chase()
    {
        agent.SetDestination(target.transform.position);
    }
}
