using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dryad : Enemy
{
    [SerializeField]
    private GameObject specialAttack;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        myCollider = GetComponent<CapsuleCollider>();

        state = new Dictionary<EnemyState, State<Enemy>>();
        state.Add(EnemyState.Idle, new DryadStates.EnemyIdle());
        state.Add(EnemyState.Move, new DryadStates.EnemyMove());
        state.Add(EnemyState.Attack, new DryadStates.EnemyAttack());
        state.Add(EnemyState.SkillA, new DryadStates.EnemySkillA());
        state.Add(EnemyState.Hit, new DryadStates.EnemyHit());
        state.Add(EnemyState.Die, new DryadStates.EnemyDie());

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
    
    public void SkillAttack()
    {
        DryadMeteor meteor = PoolManager.Instance.Get(specialAttack).GetComponent<DryadMeteor>();
        meteor.transform.position = this.gameObject.transform.position;      
        meteor.Set((this.damage * 2));
    }
}
