using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dryad : Enemy
{
    public int pattern;

    public new Dictionary<BossState, State<Dryad>> state;
    public new StateMachine<Dryad> machine;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        myCollider = GetComponent<CapsuleCollider>();

        state = new Dictionary<BossState, State<Dryad>>();
        state.Add(BossState.Idle, new DryadStates.EnemyIdle());
        state.Add(BossState.Move, new DryadStates.EnemyMove());
        state.Add(BossState.Attack, new DryadStates.EnemyAttack());
        state.Add(BossState.SkillA, new DryadStates.EnemySkillA());
        state.Add(BossState.Hit, new DryadStates.EnemyHit());
        state.Add(BossState.Die, new DryadStates.EnemyDie());

        machine = new StateMachine<Dryad>();
        machine.Init(this, state[BossState.Idle]);
    }

    private void Update()
    {
        machine.Update();
    }

    public void Attack()
    {

    }

    public void ChangeState(State<Dryad> newState)
    {
        machine.ChangeState(newState);
    }

    public new void ChangeBeforeState()
    {
        machine.ChangeBefore();
    }

    public new void Die()
    {
        isAlive = false;
        agent.isStopped = true;
        myCollider.enabled = false;
    }
}
