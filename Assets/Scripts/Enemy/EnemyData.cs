using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyData : MonoBehaviour
{
    public ParticleSystem hitEffect;
    [SerializeField]
    private GameObject text;
    [SerializeField]
    private Canvas canvas;

    public MonsterData monster;

    [HideInInspector]
    public int curHP;
    [HideInInspector]
    public int maxHP;
    [HideInInspector]
    public int damage;
    [HideInInspector]
    public int defence;
    [HideInInspector]
    public float attackSpeed;
    [HideInInspector]
    public float attackRange;

    [HideInInspector]
    public bool isAlive = true;
    [HideInInspector]
    public bool findTarget = false;
    [HideInInspector]
    public bool canAttack = false;

    public NavMeshAgent agent;

    private DamageText damageText;

    public UnityEvent<Enemy> killEvent;
    public GameObject damagePos;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        curHP = monster.HP;
        maxHP = monster.HP;
        damage = monster.damage;
        defence = monster.defence;
        attackSpeed = monster.attackSpeed;
        attackRange = monster.attackRange;
        agent.speed = monster.moveSpeed;
        canAttack = false;
        findTarget = false;
        isAlive = true;
    }

    public void DamageText(bool critical)
    {
        GameObject instance = PoolManager.Instance.Get(text);
        damageText = instance.GetComponent<DamageText>();
        damageText.SetText(PlayerStatManager.Instance.CalculateDamage(monster, critical), critical, Elemental.None);
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
}
