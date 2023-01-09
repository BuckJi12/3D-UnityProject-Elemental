using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDischarge : Skill
{
    public override void UseSkill(GameObject player)
    {
        Collider[] colliders = Physics.OverlapSphere(player.transform.position, 3, LayerMask.GetMask("Monster"));

        if (colliders == null)
            return;

        if (colliders.Length < 1)
            return;

        for (int i = 0; i < colliders.Length; i++)
        {
            ISkillHitAble skillHitAble = colliders[i].GetComponent<ISkillHitAble>();
            skillHitAble?.HitSkill(this);
        }
    }
}
