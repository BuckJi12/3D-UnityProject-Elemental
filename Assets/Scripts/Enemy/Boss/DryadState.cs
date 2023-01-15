using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

namespace DryadStates
{
    public class EnemyIdle : State<Dryad>
    {
        public override void Enter(Dryad entity)
        {
            entity.anim.SetTrigger("Idle");
            entity.agent.isStopped = true;
        }
        public override void Update(Dryad entity)
        {
            if (entity.findTarget == true)
            {
                entity.ChangeState(entity.state[BossState.Move]);
            }
        }

        public override void Exit(Dryad entity)
        {

        }
    }

    public class EnemyMove : State<Dryad>
    {
        public override void Enter(Dryad entity)
        {
            entity.anim.SetTrigger("Move");
            entity.agent.isStopped = false;
        }
        public override void Update(Dryad entity)
        {
            entity.agent.SetDestination(entity.target.transform.position);

            if (entity.canAttack == true)
            {
                entity.ChangeState(entity.state[BossState.Attack]);
            }

            if (entity.findTarget == false)
            {
                entity.ChangeState(entity.state[BossState.Idle]);
            }
        }

        public override void Exit(Dryad entity)
        {
            entity.agent.isStopped = true;
        }
    }

    public class EnemyAttack : State<Dryad>
    {
        float attackDelay;
        public override void Enter(Dryad entity)
        {

        }

        public override void Update(Dryad entity)
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
                entity.ChangeState(entity.state[BossState.Move]);
            }

            if (entity.pattern <= 10)
            {
                entity.ChangeState(entity.state[BossState.SkillA]);
            }
        }
        public override void Exit(Dryad entity)
        {

        }

        public void RandomPattern(Dryad entity)
        {
            entity.pattern = Random.Range(1, 100);
        }
    }

    public class EnemySkillA : State<Dryad>
    {
        float attackDelay;
        public override void Enter(Dryad entity)
        {
            entity.anim.SetTrigger("SkillA");
            entity.StartCoroutine(ReturnState(entity));
        }

        public override void Update(Dryad entity)
        {

        }
        public override void Exit(Dryad entity)
        {

        }
        
        public IEnumerator ReturnState(Dryad entity)
        {
            yield return new WaitForSeconds(10);
            entity.ChangeState(entity.state[BossState.Idle]);
        }
    }

    public class EnemyHit : State<Dryad>
    {
        public override void Enter(Dryad entity)
        {
            entity.anim.SetTrigger("Hit");
            entity.ChangeState(entity.state[BossState.Idle]);
        }

        public override void Update(Dryad entity)
        {

        }

        public override void Exit(Dryad entity)
        {

        }

        public IEnumerator Delay(Dryad entity)
        {
            yield return new WaitForSeconds(0.5f);
            entity.ChangeBeforeState();
        }
    }

    public class EnemyDie : State<Dryad>
    {
        public override void Enter(Dryad entity)
        {
            entity.anim.SetTrigger("Die");
        }

        public override void Update(Dryad entity)
        {

        }

        public override void Exit(Dryad entity)
        {

        }
    }
}