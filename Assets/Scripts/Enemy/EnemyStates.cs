using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyStates
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
            if (entity.data.findTarget == true)
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

            if (entity.data.canAttack == true)
            {
                entity.ChangeState(entity.state[EnemyState.Attack]);
            }

            if (entity.data.findTarget == false)
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
        float attackDelay;
        public override void Enter(Enemy entity)
        {
        }

        public override void Update(Enemy entity)
        {
            entity.transform.LookAt(entity.target.transform);

            if (Time.time > attackDelay)
            {
                attackDelay = Time.time + entity.data.attackSpeed;
                entity.anim.SetTrigger("Attack");
            }

            if (entity.data.canAttack == false)
            {
                entity.ChangeState(entity.state[EnemyState.Move]);
            }
        }
        public override void Exit(Enemy entity)
        {
            
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
