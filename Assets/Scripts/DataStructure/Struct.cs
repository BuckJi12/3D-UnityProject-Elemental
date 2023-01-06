using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Stat
{
    public int curHP;
    public int maxHP;

    public int curStamina;
    public int maxStamina;

    public int damage;
    public int elementalPower;

    public int defence;

    public float curEXP;
    public float maxEXP;

    public int criticalPercent;
    public int criticalDamage;

    public int dodgeRate;
}
