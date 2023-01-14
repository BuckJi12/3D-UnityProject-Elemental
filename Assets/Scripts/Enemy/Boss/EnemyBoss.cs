using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class EnemyBoss : MonoBehaviour
{
    private Rigidbody rigid;
    [HideInInspector]
    public NavMeshAgent agent;
    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public CapsuleCollider myCollider;

    public Dictionary<BossState, State<EnemyBoss>> state;
    public StateMachine<EnemyBoss> machine;

    public GameObject target;

    public MonsterData data;
    public int curHP;
    public int maxHP;
    public int damage;
    public int defence;
    public float attackSpeed;
    public float attackRange;


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

        state = new Dictionary<BossState, State<EnemyBoss>>();
        //state.Add(BossState.Idle, new EnemyStates.EnemyIdle());
        //state.Add(BossState.Move, new EnemyStates.EnemyMove());
        //state.Add(BossState.Attack, new EnemyStates.EnemyAttack());
        //state.Add(BossState.Hit, new EnemyStates.EnemyHit());
        //state.Add(BossState.Die, new EnemyStates.EnemyDie());

        machine = new StateMachine<EnemyBoss>();
        machine.Init(this, state[BossState.Idle]);
    }

    private void Update()
    {
        machine.Update();
    }

    public void ChangeState(State<EnemyBoss> newState)
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

