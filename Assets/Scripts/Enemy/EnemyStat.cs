using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security;
using TMPro;
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
    private GameObject damagePos;
    [SerializeField]
    private GameObject coin;
    [SerializeField]
    private GameObject weapon;
    [SerializeField]
    private GameObject armor;
    [SerializeField]
    private GameObject material;

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

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        Expansion();
    }

    public void Init()
    {
        enemy.curHP = enemy.data.HP;
        enemy.maxHP = enemy.data.HP;
        enemy.damage = enemy.data.damage;
        enemy.defence = enemy.data.defence;
        enemy.attackSpeed = enemy.data.attackSpeed;
        enemy.attackRange = enemy.data.attackRange;
        enemy.agent.speed = enemy.data.moveSpeed;
    }

    public IEnumerator DisappearObject()
    {
        yield return new WaitForSeconds(3);
        this.gameObject.SetActive(false);
        Invoke("Respawn", 20f);
    }

    public void Respawn()
    {
        enemy.Respawn();
        this.gameObject.SetActive(true);
        enemy.ChangeState(enemy.state[EnemyState.Idle]);
        enemy.canAttack = false;
        enemy.findTarget = false;
        Init();
    }

    public void TakeDamage(int damage)
    {
        if (enemy.isAlive)
        {
            if (enemy.data.canKnockBack)
            {
                enemy.ChangeState(enemy.state[EnemyState.Hit]);
                gameObject.transform.Translate(Vector3.back * 15 * Time.deltaTime);
            }
            hitEffect.Play();
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
                DropItem();
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
        instance.transform.SetParent(canvas.transform);
        instance.transform.position = damagePos.transform.position;
    }

    public void ReactionText(string text, byte r, byte g, byte b, float size)
    {
        GameObject instance = PoolManager.Instance.Get(this.text);
        damageText = instance.GetComponent<DamageText>();
        damageText.ReActionText(text, r, g, b, size);
        instance.transform.SetParent(canvas.transform);
        instance.transform.localPosition = damagePos.transform.localPosition;
    }

    public void SkillDamageText(int damage, bool critical, Elemental type)
    {
        GameObject instance = PoolManager.Instance.Get(text);
        damageText = instance.GetComponent<DamageText>();
        damageText.SetText(damage, critical, type);
        instance.transform.SetParent(canvas.transform);
        instance.transform.position = damagePos.transform.position;
    }

    public void DropMoney()
    {
        GameObject instance = PoolManager.Instance.Get(coin);
        Money money = instance.GetComponent<Money>();
        money.SetMoney(enemy.data.money);
        instance.transform.position = damagePos.transform.position;
    }

    public void DropItem()
    {
        if (enemy.data.dropItems != null)
        {
            for (int i = 0; i < enemy.data.dropItems.Count; i++)
            {
                int random = Random.Range(1, 100);
                if (enemy.data.dropItems[i].dropRate >= random)
                {
                    ItemProp prop = CreateItem(enemy.data.dropItems[i].itemData).GetComponent<ItemProp>();
                    prop.Set(enemy.data.dropItems[i].itemData);
                    prop.transform.position = transform.position;
                }
                else
                {
                    continue;
                }
            }
        }
    }

    public GameObject CreateItem(ItemData data)
    {
        if (data.kind == ItemKind.Equipment)
        {
            if (data.equipKind == EquipmentKind.Weapon)
            {
                return PoolManager.Instance.Get(weapon);
            }
            else
            {
                return PoolManager.Instance.Get(armor);
            }
        }
        else
        {
            return PoolManager.Instance.Get(material);
        }
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
                DropItem();
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
            case Elemental.None:    // �Ϲ� ����
                // ��ų�� ���Ҹ� �ְ� �ش� ������ ������� �Ǵ�
                if (skill.data.type == Elemental.Ice)
                {
                    StartCoroutine(FrostBite());
                    enemy.elementalState = Elemental.Ice;
                    ReactionText("����", 133, 245, 242, 0.2f);
                    infoUI.UpdateIcon();
                }
                if (skill.data.type == Elemental.Fire)
                {
                    StartCoroutine(FireDotDamage());
                    enemy.elementalState = Elemental.Fire;
                    ReactionText("ȭ��", 255, 85, 32, 0.2f);
                    infoUI.UpdateIcon();
                }
                break;
            case Elemental.Ice:     // ���� ����
                // �Ϲ� ���� �� ������
                // ȭ�� ���� �� ���� & ȭ�� ���·� ����
                if (skill.data.type == Elemental.Fire)
                {
                    enemy.elementalState = (Elemental.Ice | Elemental.Fire);
                }
                break;
            case Elemental.Fire:    // ȭ�� ����
                // �Ϲ� ���ݽ� ������
                // ���� ���ݽ� ���� & ȭ�� ���·� ����
                if (skill.data.type == Elemental.Ice)
                {
                    enemy.elementalState = (Elemental.Ice | Elemental.Fire);
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
            enemy.curHP -= (PlayerStatManager.Instance.stat.elementalPower / 100);
            SkillDamageText(PlayerStatManager.Instance.stat.elementalPower / 100, false, Elemental.Fire);

            // �ؽ�Ʈ ���
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
        if ((enemy.elementalState & (Elemental.Ice | Elemental.Fire)) 
            == (Elemental.Ice | Elemental.Fire))
        {
            StopAllCoroutines();
            enemy.curHP -= (PlayerStatManager.Instance.stat.elementalPower * 10);
            SkillDamageText(PlayerStatManager.Instance.stat.elementalPower * 10, false, Elemental.Fire);
            enemy.elementalState = Elemental.None;
            ReactionText("��â", 225, 125, 85, 0.3f);
            infoUI.UpdateIcon();
        }
    }
}
