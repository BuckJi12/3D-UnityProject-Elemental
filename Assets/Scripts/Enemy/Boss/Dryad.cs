using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dryad : Enemy, IDamageable, ISkillHitAble
{
    [SerializeField]
    private GameObject specialAttack;

    private void Awake()
    {
        data = GetComponent<EnemyData>();
        ele = GetComponent<ElementalReation>();
        rigid = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        myCollider = GetComponent<CapsuleCollider>();

        state = new Dictionary<EnemyState, State<Enemy>>();
        state.Add(EnemyState.Idle, new DryadStates.EnemyIdle());
        state.Add(EnemyState.Move, new DryadStates.EnemyMove());
        state.Add(EnemyState.Attack, new DryadStates.EnemyAttack());
        state.Add(EnemyState.SkillA, new DryadStates.EnemySkillA());
        state.Add(EnemyState.Hit, new DryadStates.EnemyHit());
        state.Add(EnemyState.Die, new DryadStates.EnemyDie());

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
    
    public void SkillAttack()
    {
        DryadMeteor meteor = PoolManager.Instance.Get(specialAttack).GetComponent<DryadMeteor>();
        meteor.transform.position = this.gameObject.transform.position;      
        meteor.Set((data.damage * 2));
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
                killEvent?.Invoke(this);
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
                ele.ElementalReaction(skill);
            }
            else
            {
                data.curHP -= PlayerStatManager.Instance.CalculateSkillDamage(data.monster, skill, false);
                data.SkillDamageText(PlayerStatManager.Instance.CalculateSkillDamage(data.monster, skill, false), false, skill.data.type);
                ele.ElementalReaction(skill);
            }

            if (data.curHP <= 0)
            {
                Die();
                DropMoney();
                DropItem();
                killEvent?.Invoke(this);
                PlayerStatManager.Instance.CalculateEXP(data.monster.exp);
                ChangeState(state[EnemyState.Die]);
            }
        }
    }
}
