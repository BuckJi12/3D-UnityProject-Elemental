using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/SkillData")]
public class SkillData : ScriptableObject
{
    public new string name;
    public string description;
    public string animName;

    public int canLevel;
    public Elemental type;
    public int damage;
    public float coolTime;
}
