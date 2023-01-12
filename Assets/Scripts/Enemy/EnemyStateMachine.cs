using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    private Enemy enemy;

    public State<Enemy>[] state;
    public StateMachine<Enemy> machine;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();

        state = new State<Enemy>[5];
        state[(int)EnemyState2.Idle] = new EnemyStates.EnemyIdle();
        state[(int)EnemyState2.Move] = new EnemyStates.EnemyMove();
        state[(int)EnemyState2.Attack] = new EnemyStates.EnemyAttack();
        state[(int)EnemyState2.Hit] = new EnemyStates.EnemyHit();
        state[(int)EnemyState2.Die] = new EnemyStates.EnemyDie();


        machine = new StateMachine<Enemy>();
        machine.Init(enemy, state[(int)EnemyState2.Idle]);
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
}
