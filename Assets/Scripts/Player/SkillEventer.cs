using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SkillEventer : MonoBehaviour
{
    public UnityEvent Skill1;
    public UnityEvent Skill2;


    public void UseSkill1()
    {
        Skill1?.Invoke();
    }

    public void UseSkill2()
    {
        Skill2?.Invoke();
    }
}
