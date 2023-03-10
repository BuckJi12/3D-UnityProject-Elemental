using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlantaQueen : Enemy
{
    [SerializeField]
    private GameObject energyBall;
    [SerializeField]
    private Transform attackStartPos;

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
        EnergyBall energyBall = PoolManager.Instance.Get(this.energyBall).GetComponent<EnergyBall>();
        energyBall.transform.position = attackStartPos.position;
        energyBall.Set(this.damage, this.gameObject);
    }
}
