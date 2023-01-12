using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    private Rigidbody rigid;
    public NavMeshAgent agent;
    public Animator anim;
    public EnemyStateMachine stateMachine;

    public GameObject target;

    public MonsterData data;
    public int curHP;
    public int maxHP;
    public int damage;
    public int defence;

    [HideInInspector]
    public bool isAlive = true;
    [HideInInspector]
    public bool findTarget = false;
    [HideInInspector]
    public bool canAttack = false;

    public Elemental elementalState;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        stateMachine = GetComponent<EnemyStateMachine>();
    }
}
