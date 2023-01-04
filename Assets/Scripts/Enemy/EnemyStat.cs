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

    private Rigidbody rigid;
    private Animator anim;

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
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        if (enemyAI.isAlive)
        {
            anim.SetTrigger("Hit");
            hitEffect.Play();
            gameObject.transform.Translate(Vector3.back * 15 * Time.deltaTime);
            //rigid.AddForce(Vector3.back * 2, ForceMode.Impulse);

            GameObject damageText = PoolManager.Instance.Get(text);
            damageText.transform.position = damagePos.transform.position;
            damageText.transform.SetParent(canvas.transform);

            curHP -= PlayerStatManager.Instance.CalculateCritical();

            if (curHP <= 0)
            {
                enemyAI.Die();
                anim.SetTrigger("Die");
                StartCoroutine(DisappearObject());
            }
        }
    }
}
