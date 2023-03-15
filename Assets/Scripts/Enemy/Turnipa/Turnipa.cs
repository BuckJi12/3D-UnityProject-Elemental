using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Turnipa : Enemy, IDamageable, ISkillHitAble
{
    private void Awake()
    {
        data = GetComponent<EnemyData>();
        ele = GetComponent<ElementalReaction>();
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        myCollider = GetComponent<CapsuleCollider>();
        target = GameObject.FindWithTag("Player");

        state = new Dictionary<EnemyState, State<Enemy>>();
        state.Add(EnemyState.Idle, new EnemyStates.EnemyIdle());
        state.Add(EnemyState.Move, new EnemyStates.EnemyMove());
        state.Add(EnemyState.Attack, new EnemyStates.EnemyAttack());
        state.Add(EnemyState.Hit, new EnemyStates.EnemyHit());
        state.Add(EnemyState.Die, new EnemyStates.EnemyDie());

        machine = new StateMachine<Enemy>();
        machine.Init(this, state[EnemyState.Idle]);
    }

    public override void Attack()
    {
        if (data.isAlive)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, data.attackRange, LayerMask.GetMask("Player"));

            if (colliders == null)
                return;

            if (colliders.Length < 1)
                return;

            IDamageable damageable = target.GetComponent<IDamageable>();
            damageable?.TakeDamage(data.damage);
        }
    }

    public void TakeDamage(int damage = 0)
    {
        if (data.isAlive)
        {
            if (data.monster.canKnockBack)
            {
                ChangeState(state[EnemyState.Hit]);
                gameObject.transform.Translate(Vector3.back * 15 * Time.deltaTime);
            }
            data.hitEffect.Play();
            if (PlayerStatManager.Instance.CalculateCritical())
            {
                data.curHP -= PlayerStatManager.Instance.CalculateDamage(data.monster, true);
                data.DamageText(true);
            }
            else
            {
                data.curHP -= PlayerStatManager.Instance.CalculateDamage(data.monster, false);
                data.DamageText(false);
            }

            if (data.curHP <= 0)
            {
                Die();
                DropMoney();
                DropItem();
                QuestManager.Instance.KillRequestUpdate(this);
                PlayerStatManager.Instance.CalculateEXP(data.monster.exp);
                ChangeState(state[EnemyState.Die]);
            }
        }
    }

    public void HitSkill(Skill skill)
    {
        if (data.isAlive)
        {
            ChangeState(state[EnemyState.Hit]);
            data.hitEffect.Play();
            gameObject.transform.Translate(Vector3.back * 15 * Time.deltaTime);
            if (PlayerStatManager.Instance.CalculateCritical())
            {
                data.curHP -= PlayerStatManager.Instance.CalculateSkillDamage(data.monster, skill, true);
                data.SkillDamageText(PlayerStatManager.Instance.CalculateSkillDamage(data.monster, skill, true), true, skill.data.type);
                ele.Reaction(skill);
            }
            else
            {
                data.curHP -= PlayerStatManager.Instance.CalculateSkillDamage(data.monster, skill, false);
                data.SkillDamageText(PlayerStatManager.Instance.CalculateSkillDamage(data.monster, skill, false), false, skill.data.type);
                ele.Reaction(skill);
            }

            if (data.curHP <= 0)
            {
                Die();
                DropMoney();
                DropItem();
                QuestManager.Instance.KillRequestUpdate(this);
                PlayerStatManager.Instance.CalculateEXP(data.monster.exp);
                ChangeState(state[EnemyState.Die]);
            }
        }
    }
}
