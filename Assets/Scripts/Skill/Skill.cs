using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public SkillData data;

    public void Start()
    {
        SkillManager.Instance.haveSkill.Add(this);
    }

    public virtual void UseSkill(GameObject player) { }
}
