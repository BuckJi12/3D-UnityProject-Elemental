using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

public class EnemyStat : MonoBehaviour, IDamageable, ISkillHitAble
{
    [SerializeField]
    private ParticleSystem hitEffect;
    [SerializeField]
    private GameObject text;
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private Transform damagePos;
    [SerializeField]
    private GameObject coin;

    private Enemy enemy;

    private Rigidbody rigid;
    private Animator anim;
    private DamageText damageText;
    private EnemyInfoUI infoUI;

    public UnityEvent<Enemy> killEvent;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
        infoUI = GetComponent<EnemyInfoUI>();
        enemy = GetComponent<Enemy>();
    }
    private void Update()
    {
        Expansion();
    }
    public IEnumerator DisappearObject()
    {
        yield return new WaitForSeconds(3);
        this.gameObject.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        if (enemy.isAlive)
        {
            enemy.ChangeState(enemy.state[EnemyState.Hit]);
            hitEffect.Play();
            gameObject.transform.Translate(Vector3.back * 15 * Time.deltaTime);
            if (PlayerStatManager.Instance.CalculateCritical())
            {
                enemy.curHP -= PlayerStatManager.Instance.CalculateDamage(enemy, true);
                DamageText(true);
            }
            else
            {
                enemy.curHP -= PlayerStatManager.Instance.CalculateDamage(enemy, false);
                DamageText(false);
            }

            if (enemy.curHP <= 0)
            {
                enemy.Die();
                DropMoney();
                killEvent?.Invoke(enemy);
                PlayerStatManager.Instance.CalculateEXP(enemy.data.exp);
                enemy.ChangeState(enemy.state[EnemyState.Die]);
                StartCoroutine(DisappearObject());
            }
        }
    }

    public void DamageText(bool critical)
    {
        GameObject instance = PoolManager.Instance.Get(text);
        damageText = instance.GetComponent<DamageText>();
        damageText.SetText(PlayerStatManager.Instance.CalculateDamage(enemy, critical), critical, Elemental.None);
        instance.transform.position = damagePos.transform.position;
        instance.transform.SetParent(canvas.transform);
    }

    public void ReactionText(string text, byte r, byte g, byte b, float size)
    {
        GameObject instance = PoolManager.Instance.Get(this.text);
        damageText = instance.GetComponent<DamageText>();
        damageText.ReActionText(text, r, g, b, size);
        instance.transform.position = damagePos.transform.position;
        instance.transform.SetParent(canvas.transform);
    }

    public void SkillDamageText(int damage, bool critical, Elemental type)
    {
        GameObject instance = PoolManager.Instance.Get(text);
        damageText = instance.GetComponent<DamageText>();
        damageText.SetText(damage, critical, type);
        instance.transform.position = damagePos.transform.position;
        instance.transform.SetParent(canvas.transform);
    }

    public void DropMoney()
    {
        GameObject instance = PoolManager.Instance.Get(coin);
        Money money = instance.GetComponent<Money>();
        money.SetMoney(enemy.data.money);
        instance.transform.position = damagePos.transform.position;
    }

    public void HitSkill(Skill skill)
    {
        if (enemy.isAlive)
        {
            enemy.ChangeState(enemy.state[EnemyState.Hit]);
            hitEffect.Play();
            gameObject.transform.Translate(Vector3.back * 15 * Time.deltaTime);
            if (PlayerStatManager.Instance.CalculateCritical())
            {
                enemy.curHP -= PlayerStatManager.Instance.CalculateSkillDamage(enemy, skill, true);
                SkillDamageText(PlayerStatManager.Instance.CalculateSkillDamage(enemy, skill, true), true, skill.data.type);
                ElementalReaction(skill);
            }
            else
            {
                enemy.curHP -= PlayerStatManager.Instance.CalculateSkillDamage(enemy, skill, false);
                SkillDamageText(PlayerStatManager.Instance.CalculateSkillDamage(enemy, skill, false), false, skill.data.type);
                ElementalReaction(skill);
            }

            if (enemy.curHP <= 0)
            {
                enemy.Die();
                DropMoney();
                killEvent?.Invoke(enemy);
                PlayerStatManager.Instance.CalculateEXP(enemy.data.exp);
                enemy.ChangeState(enemy.state[EnemyState.Die]);
                StartCoroutine(DisappearObject());
            }
        }
    }

    public void ElementalReaction(Skill skill)
    {
        switch (enemy.elementalState)
        {
            case Elemental.None:    // 일반 상태
                // 스킬의 원소를 주고 해당 원소의 디버프를 건다
                if (skill.data.type == Elemental.Ice)
                {
                    StartCoroutine(FrostBite());
                    enemy.elementalState = Elemental.Ice;
                    ReactionText("동상", 133, 245, 242, 0.5f);
                    infoUI.UpdateIcon();
                }
                if (skill.data.type == Elemental.Fire)
                {
                    StartCoroutine(FireDotDamage());
                    enemy.elementalState = Elemental.Fire;
                    ReactionText("화상", 255, 85, 32, 0.5f);
                    infoUI.UpdateIcon();
                }
                break;
            case Elemental.Ice:     // 얼음 상태
                // 일반 공격 시 무반응
                // 화염 공격 시 얼음 & 화염 상태로 적용
                if (skill.data.type == Elemental.Fire)
                {
                    enemy.elementalState = (Elemental.Ice | Elemental.Fire);
                }
                break;
            case Elemental.Fire:    // 화염 상태
                // 일반 공격시 무반응
                // 얼음 공격시 얼음 & 화염 상태로 적용
                if (skill.data.type == Elemental.Ice)
                {
                    enemy.elementalState = (Elemental.Ice | Elemental.Fire);
                }
                break;
            case Elemental.Lightning:   // 번개 상태 보류
                break;
        }
    }

    public IEnumerator FireDotDamage()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(2);
            enemy.curHP -= (PlayerStatManager.Instance.stat.elementalPower / 100);
            SkillDamageText(PlayerStatManager.Instance.stat.elementalPower / 100, false, Elemental.Fire);

            // 텍스트 출력
        }

        enemy.elementalState = Elemental.None;
        infoUI.UpdateIcon();
    }

    public IEnumerator FrostBite()
    {
        int originalDefence = enemy.defence;
        enemy.defence = (int)(enemy.defence * 0.7f);
        enemy.agent.speed = 1;


        yield return new WaitForSeconds(10);
        enemy.elementalState = Elemental.None;
        infoUI.UpdateIcon();
        enemy.defence = originalDefence;
        enemy.agent.speed = 2;
    }

    public void Expansion()
    {
        if (enemy.elementalState == (Elemental.Ice | Elemental.Fire))
        {
            StopAllCoroutines();
            enemy.curHP -= (PlayerStatManager.Instance.stat.elementalPower * 10);
            SkillDamageText(PlayerStatManager.Instance.stat.elementalPower * 10, false, Elemental.Fire);
            enemy.elementalState = Elemental.None;
            ReactionText("팽창", 225, 125, 85, 0.7f);
            infoUI.UpdateIcon();
        }
    }
}
