using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public SkillData data;
    public float coolTime;
    public bool canUse;

    public void Start()
    {
        SkillManager.Instance.haveSkill.Add(this);
        coolTime = data.coolTime;
        canUse = true;
    }

    public virtual void UseSkill(GameObject player) { }

    public void StartCoolTime()
    {
        canUse = false;
        StartCoroutine(CoolTime());
    }

    public IEnumerator CoolTime()
    {
        yield return new WaitForSeconds(data.coolTime);
        canUse = true;
    }
}
