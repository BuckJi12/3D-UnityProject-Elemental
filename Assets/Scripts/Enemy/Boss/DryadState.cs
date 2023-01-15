using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

namespace DryadStates
{
    public class EnemyIdle : State<Enemy>
    {
        public override void Enter(Enemy entity)
        {
            entity.anim.SetTrigger("Idle");
            entity.agent.isStopped = true;
        }
        public override void Update(Enemy entity)
        {
            if (entity.findTarget == true)
            {
                entity.ChangeState(entity.state[EnemyState.Move]);
            }
        }

        public override void Exit(Enemy entity)
        {

        }
    }

    public class EnemyMove : State<Enemy>
    {
        public override void Enter(Enemy entity)
        {
            entity.anim.SetTrigger("Move");
            entity.agent.isStopped = false;
        }
        public override void Update(Enemy entity)
        {
            entity.agent.SetDestination(entity.target.transform.position);

            if (entity.canAttack == true)
            {
                entity.ChangeState(entity.state[EnemyState.Attack]);
            }

            if (entity.findTarget == false)
            {
                entity.ChangeState(entity.state[EnemyState.Idle]);
            }
        }

        public override void Exit(Enemy entity)
        {
            entity.agent.isStopped = true;
        }
    }

    public class EnemyAttack : State<Enemy>
    {
        public float attackDelay;
        public int pattern;
        public override void Enter(Enemy entity)
        {

        }

        public override void Update(Enemy entity)
        {
            entity.transform.LookAt(entity.target.transform);

            if (Time.time > attackDelay)
            {
                attackDelay = Time.time + entity.attackSpeed;
                entity.anim.SetTrigger("Attack");
                RandomPattern(entity);
            }

            if (entity.canAttack == false)
            {
                entity.ChangeState(entity.state[EnemyState.Move]);
            }

            if (pattern <= 10)
            {
                entity.ChangeState(entity.state[EnemyState.SkillA]);
            }
        }
        public override void Exit(Enemy entity)
        {

        }

        public void RandomPattern(Enemy entity)
        {
            pattern = Random.Range(1, 100);
        }
    }

    public class EnemySkillA : State<Enemy>
    {
        float attackDelay;
        public override void Enter(Enemy entity)
        {
            entity.anim.SetTrigger("SkillA");
            entity.StartCoroutine(ReturnState(entity));
        }

        public override void Update(Enemy entity)
        {

        }
        public override void Exit(Enemy entity)
        {

        }
        
        public IEnumerator ReturnState(Enemy entity)
        {
            yield return new WaitForSeconds(10);
            entity.ChangeState(entity.state[EnemyState.Idle]);
        }
    }

    public class EnemyHit : State<Enemy>
    {
        public override void Enter(Enemy entity)
        {
            entity.anim.SetTrigger("Hit");
            entity.ChangeState(entity.state[EnemyState.Idle]);
        }

        public override void Update(Enemy entity)
        {

        }

        public override void Exit(Enemy entity)
        {

        }

        public IEnumerator Delay(Enemy entity)
        {
            yield return new WaitForSeconds(0.5f);
            entity.ChangeBeforeState();
        }
    }

    public class EnemyDie : State<Enemy>
    {
        public override void Enter(Enemy entity)
        {
            entity.anim.SetTrigger("Die");
        }

        public override void Update(Enemy entity)
        {

        }

        public override void Exit(Enemy entity)
        {

        }
    }
}