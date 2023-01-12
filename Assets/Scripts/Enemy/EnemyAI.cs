using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    [HideInInspector]
    public NavMeshAgent agent;
    private Detect detect;
    private AttackRange attackRange;
    private Animator anim;
    private EnemyStat stat;

    private Collider myCollider;
    private GameObject target;

    [SerializeField]
    private LayerMask targetMask;

    private EnemyState enemyState;

    [SerializeField]
    private float attackSpeed;

    private float attackDelay;

    public bool isAlive = true;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        detect = GetComponentInChildren<Detect>();
        attackRange = GetComponentInChildren<AttackRange>();
        stat = GetComponent<EnemyStat>();
        target = GameObject.FindGameObjectWithTag("Player");
        myCollider = GetComponent<CapsuleCollider>();
    }

    private void Start()
    {
        enemyState = EnemyState.Normal;
    }

    private void Update()
    {
        if (isAlive)
        {
            switch (enemyState)
            {
                case EnemyState.Normal:
                    break;
                case EnemyState.Combat:
                    if (attackRange.canAttack)
                    {
                        Attack();
                    }
                    else
                    {
                        Chase();
                    }
                    break;
            }
        }
    }

    public void SwitchMode()
    {
            if (enemyState == EnemyState.Normal)
            {
                enemyState = EnemyState.Combat;
                transform.LookAt(target.transform);
            }
            else if (enemyState == EnemyState.Combat)
            {
                //transform.LookAt(Vector3.zero);
                anim.SetBool("IsMove", false);
                enemyState = EnemyState.Normal;
                agent.SetDestination(transform.parent.position);
            }
    }

    public void Attack()
    {
        agent.isStopped = true;
        anim.SetBool("IsMove", false);
        if (Time.time > attackDelay)
        {
            attackDelay = Time.time + attackSpeed;
            anim.SetTrigger("Attack");
        }
    }
    public void Chase()
    {
        anim.SetBool("IsMove", true);
        agent.isStopped = false;
        agent.SetDestination(target.transform.position);
    }

    public void OnAttackCollider()
    {
        if (isAlive)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange.attackRange.radius, targetMask);

            if (colliders == null)
                return;

            if (colliders.Length < 1)
                return;

            IDamageable damageable = target.GetComponent<IDamageable>();
            //damageable?.TakeDamage(stat.damage);
        }   
    }

    public void Die()
    {
        isAlive = false;
        agent.isStopped = true;
        detect.detectRange.enabled = false;
        attackRange.enabled = false;
        myCollider.enabled = false;
    }
}
