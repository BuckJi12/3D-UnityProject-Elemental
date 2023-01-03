using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatManager : SingleTon<PlayerStatManager>
{
    public int curHP;
    public int maxHP;

    public int damage;

    public int defence;

    public float curEXP;
    public float maxEXP;

    public int criticalPercent;
    public int criticalDamage;

    public int dodgeRate;

    public void CalculateDamage(int damage)
    {
        damage -= defence;
        if (damage < 0)
            return;

        curHP -= damage;

        if (curHP < 1)
        {
            // ав╬З╢ы
        }       
    }

    public void CalculateEXP(int exp)
    {
        curEXP += exp;
        if (curEXP >= maxEXP)
        {
            float remnant = curEXP - maxEXP;
            curEXP = 0;
            curEXP += remnant;
            maxEXP = maxEXP * 1.2f;
        }
    }
}
