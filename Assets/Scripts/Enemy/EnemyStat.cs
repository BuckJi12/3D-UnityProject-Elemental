using System.Collections;
using System.Collections.Generic;
using System.Security;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

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

    private Rigidbody rigid;
    private Animator anim;
    private DamageText damageText;
    private EnemyAI enemyAI;

    public Elemental enemyState;

    public MonsterData statData;
    public int curHP;
    public int maxHP;
    public int damage;
    public int defence;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
        enemyAI = GetComponent<EnemyAI>();
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
        if (enemyAI.isAlive)
        {
            anim.SetTrigger("Hit");
            hitEffect.Play();
            gameObject.transform.Translate(Vector3.back * 15 * Time.deltaTime);
            if (PlayerStatManager.Instance.CalculateCritical())
            {
                curHP -= PlayerStatManager.Instance.CalculateDamage(this, true);
                DamageText(true);
            }
            else
            {
                curHP -= PlayerStatManager.Instance.CalculateDamage(this, false);
                DamageText(false);
            }

            if (curHP <= 0)
            {
                enemyAI.Die();
                DropMoney();
                anim.SetTrigger("Die");
                StartCoroutine(DisappearObject());
            }
        }
    }

    public void DamageText(bool critical)
    {
        GameObject instance = PoolManager.Instance.Get(text);
        damageText = instance.GetComponent<DamageText>();
        damageText.SetText(PlayerStatManager.Instance.CalculateDamage(this, critical), critical, Elemental.None);
        instance.transform.position = damagePos.transform.position;
        instance.transform.SetParent(canvas.transform);
    }

    public void SkillDamageText(bool critical, Skill skill)
    {
        GameObject instance = PoolManager.Instance.Get(text);
        damageText = instance.GetComponent<DamageText>();
        damageText.SetText(PlayerStatManager.Instance.CalculateSkillDamage(this, skill, critical), critical, skill.data.type);
        instance.transform.position = damagePos.transform.position;
        instance.transform.SetParent(canvas.transform);
    }

    public void DropMoney()
    {
        GameObject instance = PoolManager.Instance.Get(coin);
        Money money = instance.GetComponent<Money>();
        money.SetMoney(statData.money);
        instance.transform.position = damagePos.transform.position;
    }

    public void HitSkill(Skill skill)
    {
        if (enemyAI.isAlive)
        {
            anim.SetTrigger("Hit");
            hitEffect.Play();
            gameObject.transform.Translate(Vector3.back * 15 * Time.deltaTime);
            if (PlayerStatManager.Instance.CalculateCritical())
            {
                curHP -= PlayerStatManager.Instance.CalculateSkillDamage(this, skill, true);
                SkillDamageText(true, skill);
                ElementalReaction(skill);
            }
            else
            {
                curHP -= PlayerStatManager.Instance.CalculateSkillDamage(this, skill, false);
                SkillDamageText(false, skill);
                ElementalReaction(skill);
            }

            if (curHP <= 0)
            {
                enemyAI.Die();
                DropMoney();
                anim.SetTrigger("Die");
                StartCoroutine(DisappearObject());
            }
        }
    }

    public void ElementalReaction(Skill skill)
    {
        switch (enemyState)
        {
            case Elemental.None:    // �Ϲ� ����
                // ��ų�� ���Ҹ� �ְ� �ش� ������ ������� �Ǵ�
                if (skill.data.type == Elemental.Ice)
                {
                    StartCoroutine(FrostBite());
                    enemyState = Elemental.Ice;
                }
                if (skill.data.type == Elemental.Fire)
                {
                    StartCoroutine(FireDotDamage());
                    enemyState = Elemental.Fire;
                }
                break;
            case Elemental.Ice:     // ���� ����
                // �Ϲ� ���� �� ������
                // ȭ�� ���� �� ���� & ȭ�� ���·� ����
                if (skill.data.type == Elemental.Fire)
                {
                    enemyState = (Elemental.Ice | Elemental.Fire);
                }
                break;
            case Elemental.Fire:    // ȭ�� ����
                // �Ϲ� ���ݽ� ������
                // ���� ���ݽ� ���� & ȭ�� ���·� ����
                if (skill.data.type == Elemental.Ice)
                {
                    enemyState = (Elemental.Ice | Elemental.Fire);
                }
                break;
            case Elemental.Lightning:   // ���� ���� ����
                break;
        }
    }

    public IEnumerator FireDotDamage()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(2);
            curHP -= (PlayerStatManager.Instance.stat.elementalPower / 100);
            // �ؽ�Ʈ ���
        }

        enemyState = Elemental.None;
    }

    public IEnumerator FrostBite()
    {
        int originalDefence = defence;
        defence = (int)(defence * 0.7f);
        enemyAI.agent.speed = 1;


        yield return new WaitForSeconds(10);
        enemyState = Elemental.None;
        defence = originalDefence;
        enemyAI.agent.speed = 2;
    }

    public void Expansion()
    {
        if (enemyState == (Elemental.Ice | Elemental.Fire))
        {
            StopAllCoroutines();
            curHP -= (PlayerStatManager.Instance.stat.elementalPower * 5);
            enemyState = Elemental.None;
        }
    }
}
