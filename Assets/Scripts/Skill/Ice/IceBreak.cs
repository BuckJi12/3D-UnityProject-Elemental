using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBreak : Skill
{

    public override void UseSkill(GameObject player)
    {
        Collider[] colliders = Physics.OverlapSphere(player.transform.position, 10, LayerMask.GetMask("Monster"));

        if (colliders == null)
            return;

        if (colliders.Length < 1)
            return;

        for (int i = 0; i < colliders.Length; i++)
        {
            Vector3 dirToTarget = (colliders[i].transform.position - transform.position).normalized;
            Vector3 rightDir = AngleToDir(player.transform.eulerAngles.y + 120 * 0.5f);

            if (Vector3.Dot(transform.forward, dirToTarget) > Vector3.Dot(transform.forward, rightDir))
            {
                ISkillHitAble skillHitAble = colliders[i].GetComponent<ISkillHitAble>();
                skillHitAble?.HitSkill(this);
            }
        }
    }


    private Vector3 AngleToDir(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0, Mathf.Cos(radian));
    }
}
