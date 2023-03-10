using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public abstract class Enemy : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody rigid;
    [HideInInspector]
    public NavMeshAgent agent;
    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public CapsuleCollider myCollider;

    public Dictionary<EnemyState, State<Enemy>> state;
    public StateMachine<Enemy> machine;

    [HideInInspector]
    public GameObject target;

    public MonsterData data;
    [HideInInspector]
    public int curHP;
    [HideInInspector]
    public int maxHP;
    [HideInInspector]
    public int damage;
    [HideInInspector]
    public int defence;
    [HideInInspector]
    public float attackSpeed;
    [HideInInspector]
    public float attackRange;

    [HideInInspector]
    public bool isAlive = true;
    [HideInInspector]
    public bool findTarget = false;
    [HideInInspector]
    public bool canAttack = false;

    public Elemental elementalState;

    public virtual void Attack()
    {

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
        myCollider.enabled = false;
    }

    public void Respawn()
    {
        isAlive = true;
        myCollider.enabled = true;
    }
}
