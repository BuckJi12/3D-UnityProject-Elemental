using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceAge : Skill
{
    [SerializeField]
    private GameObject particle;

    public override void UseSkill(GameObject player)
    {
        particle = PoolManager.Instance.Get(particle);
        particle.transform.position = player.transform.position;
        StartCoroutine(DelayAttack(particle));
    }
    
    public IEnumerator DelayAttack(GameObject player)
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(3);
            Collider[] colliders = Physics.OverlapSphere(player.transform.position, 6, LayerMask.GetMask("Monster"));

            if (colliders == null)
                continue;

            if (colliders.Length < 1)
                continue;

            for (int j = 0; j < colliders.Length; j++)
            {
                ISkillHitAble skillHitAble = colliders[j].GetComponent<ISkillHitAble>();
                skillHitAble?.HitSkill(this);
            }
        }
    }
}
