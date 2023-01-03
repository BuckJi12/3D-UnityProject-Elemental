using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour, IDamageable
{
    private Rigidbody rigid;
    private Animator anim;

    public MonsterData statData;
    public int curHP;
    public int maxHP;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
    }
    public void Die()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        anim.SetTrigger("Hit");
        //gameObject.transform.Translate(Vector3.back * 15 * Time.deltaTime);
        rigid.AddForce(Vector3.back * 2, ForceMode.Impulse);
        //StartCoroutine(StopKnockBack());
        curHP -= PlayerStatManager.Instance.damage;

        if (curHP <= 0)
        {
           Die();
        }
    }
    
    //public IEnumerator StopKnockBack()
    //{
    //    yield return new WaitForSeconds(0.2f);
    //    rigid.velocity = Vector3.zero;
    //}
}
