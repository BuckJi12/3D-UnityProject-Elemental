using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStat : MonoBehaviour, IDamageable
{
    [SerializeField]
    private ParticleSystem hitEffect;

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
            curHP -= PlayerStatManager.Instance.damage;

            if (curHP <= 0)
            {
                enemyAI.Die();
                anim.SetTrigger("Die");
                StartCoroutine(DisappearObject());
            }
        }
    }
}
