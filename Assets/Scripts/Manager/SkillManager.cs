using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : SingleTon<SkillManager>
{
    public List<Skill> equipSkill;
    public List<Skill> haveSkill;

    [SerializeField]
    private GameObject player;

    private void Awake()
    {
        equipSkill = new List<Skill>();
        haveSkill = new List<Skill>();
    }

    public void EquipSKill(Skill skill)
    {
        equipSkill.Add(skill);
        haveSkill.Remove(skill);
    }

    public void OutSkill(Skill skill)
    {
        equipSkill.Remove(skill);
        haveSkill.Add(skill);
    }
}
