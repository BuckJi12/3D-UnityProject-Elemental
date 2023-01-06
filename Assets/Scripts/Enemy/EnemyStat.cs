using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStat : MonoBehaviour, IDamageable
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

    public MonsterData statData;
    public int curHP;
    public int maxHP;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
        enemyAI = GetComponent<EnemyAI>();
    }
    public IEnumerator DisappearObject()
    {
        yield return new WaitForSeconds(3);
        //Destroy(gameObject);
        this.gameObject.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        if (enemyAI.isAlive)
        {
            anim.SetTrigger("Hit");
            hitEffect.Play();
            gameObject.transform.Translate(Vector3.back * 15 * Time.deltaTime);
            //rigid.AddForce(Vector3.back * 2, ForceMode.Impulse);
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
        damageText.SetText(PlayerStatManager.Instance.CalculateDamage(this, critical), critical);
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
}
