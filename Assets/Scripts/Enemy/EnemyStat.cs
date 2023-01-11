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
    private EnemyInfoUI infoUI;

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
        infoUI = GetComponent<EnemyInfoUI>();
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
                SkillDamageText(PlayerStatManager.Instance.CalculateSkillDamage(this, skill, true), true, skill.data.type);
                ElementalReaction(skill);
            }
            else
            {
                curHP -= PlayerStatManager.Instance.CalculateSkillDamage(this, skill, false);
                SkillDamageText(PlayerStatManager.Instance.CalculateSkillDamage(this, skill, false), false, skill.data.type);
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
                    ReactionText("����", 133, 245, 242, 0.5f);
                    infoUI.UpdateIcon();
                }
                if (skill.data.type == Elemental.Fire)
                {
                    StartCoroutine(FireDotDamage());
                    enemyState = Elemental.Fire;
                    ReactionText("ȭ��", 255, 85, 32, 0.5f);
                    infoUI.UpdateIcon();
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
            SkillDamageText(PlayerStatManager.Instance.stat.elementalPower / 100, false, Elemental.Fire);

            // �ؽ�Ʈ ���
        }

        enemyState = Elemental.None;
        infoUI.UpdateIcon();
    }

    public IEnumerator FrostBite()
    {
        int originalDefence = defence;
        defence = (int)(defence * 0.7f);
        enemyAI.agent.speed = 1;


        yield return new WaitForSeconds(10);
        enemyState = Elemental.None;
        infoUI.UpdateIcon();
        defence = originalDefence;
        enemyAI.agent.speed = 2;
    }

    public void Expansion()
    {
        if (enemyState == (Elemental.Ice | Elemental.Fire))
        {
            StopAllCoroutines();
            curHP -= (PlayerStatManager.Instance.stat.elementalPower * 10);
            SkillDamageText(PlayerStatManager.Instance.stat.elementalPower * 10, false, Elemental.Fire);
            enemyState = Elemental.None;
            ReactionText("��â", 225, 125, 85, 0.7f);
            infoUI.UpdateIcon();
        }
    }
}
