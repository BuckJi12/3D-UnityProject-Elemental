using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public SkillData data;

    public void Start()
    {
        
    }

    public virtual void UseSkill() { }
}
