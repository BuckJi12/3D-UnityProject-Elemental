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
            if (entity.findTarget == true)
            {
                entity.stateMachine.machine.ChangeState(entity.stateMachine.state[(int)EnemyState2.Move]);
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
        }

        public override void Exit(Enemy entity)
        {
            entity.agent.isStopped = true;
        }
    }

    public class EnemyAttack : State<Enemy>
    {
        public override void Enter(Enemy entity)
        {
            entity.anim.SetTrigger("Attack");
            entity.stateMachine.ChangeBeforeState();
        }

        public override void Update(Enemy entity)
        {
            throw new System.NotImplementedException();
        }

        public override void Exit(Enemy entity)
        {
            throw new System.NotImplementedException();
        }
    }

    public class EnemyHit : State<Enemy>
    {
        public override void Enter(Enemy entity)
        {
            entity.anim.SetTrigger("Hit");
            entity.stateMachine.ChangeBeforeState();
        }

        public override void Update(Enemy entity)
        {

        }

        public override void Exit(Enemy entity)
        {

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
