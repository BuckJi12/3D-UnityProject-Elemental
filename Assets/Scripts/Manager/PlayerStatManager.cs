using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatManager : SingleTon<PlayerStatManager>
{
    public int curHP;
    public int maxHP;

    public int damage;
    public int elementalPower;

    public int defence;

    public float curEXP;
    public float maxEXP;

    public int criticalPercent;
    public int criticalDamage;

    public int dodgeRate;

    public void CalculateTakeDamage(int damage)
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

    public bool CalculateCritical()
    {
        int random = Random.Range(1, 100);
        if (random > criticalPercent)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public int CalculateDamage(EnemyStat enemystat, bool critical)
    {
        int finalDamage;
        if (critical)
        {
            finalDamage = ((this.damage * (criticalDamage + 100)) / 100) - enemystat.statData.defence;
        }
        else
        {
            finalDamage = this.damage - enemystat.statData.defence;
        }

        if (finalDamage <= 0)
            return 0;

        return finalDamage;
    }
}
