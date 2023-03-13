using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlantaQueen : Enemy, IDamageable, ISkillHitAble
{
    [SerializeField]
    private GameObject energyBall;
    [SerializeField]
    private Transform attackStartPos;

    private void Awake()
    {
        data = GetComponent<EnemyData>();
        ele = GetComponent<ElementalReation>();
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
        EnergyBall energyBall = PoolManager.Instance.Get(this.energyBall).GetComponent<EnergyBall>();
        energyBall.transform.position = attackStartPos.position;
        energyBall.Set(data.damage, this.gameObject);
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
