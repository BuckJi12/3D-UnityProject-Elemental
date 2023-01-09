using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SkillEventer : MonoBehaviour
{
    public UnityEvent Skill1;


    public void UseSkill()
    {
        Skill1?.Invoke();
    }
}
