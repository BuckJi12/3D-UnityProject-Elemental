using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    private Rigidbody rigid;
    [HideInInspector]
    public NavMeshAgent agent;
    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public CapsuleCollider myCollider;

    public Dictionary<EnemyState2, State<Enemy>> state;
    public StateMachine<Enemy> machine;

    public GameObject target;

    public MonsterData data;
    public int curHP;
    public int maxHP;
    public int damage;
    public int defence;

    
    public bool isAlive = true;
    public bool findTarget = false;
    public bool canAttack = false;

    public Elemental elementalState;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        myCollider = GetComponent<CapsuleCollider>();

        state = new Dictionary<EnemyState2, State<Enemy>>();
        state.Add(EnemyState2.Idle, new EnemyStates.EnemyIdle());
        state.Add(EnemyState2.Move, new EnemyStates.EnemyMove());
        state.Add(EnemyState2.Attack, new EnemyStates.EnemyAttack());
        state.Add(EnemyState2.Hit, new EnemyStates.EnemyHit());
        state.Add(EnemyState2.Die, new EnemyStates.EnemyDie());

        machine = new StateMachine<Enemy>();
        machine.Init(this, state[EnemyState2.Idle]);
    }

    private void Update()
    {
        machine.Update();
    }

    public void ChangeState(State<Enemy> newState)
    {
        machine.ChangeState(newState);
    }

    public void ChangeBeforeState()
    {
        machine.ChangeBefore();
    }

    public void Die()
    {
        isAlive = false;
        agent.isStopped = true;
        myCollider.enabled = false;
    }
}
