using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Turnipa : Enemy
{
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        myCollider = GetComponent<CapsuleCollider>();
        target = GameObject.FindWithTag("Player");

        state = new Dictionary<EnemyState, State<Enemy>>();
        state.Add(EnemyState.Idle, new EnemyStates.EnemyIdle());
        state.Add(EnemyState.Move, new EnemyStates.EnemyMove());
        state.Add(EnemyState.Attack, new EnemyStates.EnemyAttack());
        state.Add(EnemyState.Hit, new EnemyStates.EnemyHit());
        state.Add(EnemyState.Die, new EnemyStates.EnemyDie());

        machine = new StateMachine<Enemy>();
        machine.Init(this, state[EnemyState.Idle]);
    }

    public override void Attack()
    {
        if (this.isAlive)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, this.attackRange, LayerMask.GetMask("Player"));

            if (colliders == null)
                return;

            if (colliders.Length < 1)
                return;

            IDamageable damageable = target.GetComponent<IDamageable>();
            damageable?.TakeDamage(this.damage);
        }
    }
}
