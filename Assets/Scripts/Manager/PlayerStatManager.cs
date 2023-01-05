using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatManager : SingleTon<PlayerStatManager>
{
    public Stat stat;
    public int level = 1;

    public void CalculateTakeDamage(int damage)
    {
        int random = Random.Range(1, 100);
        if (random > stat.dodgeRate)
        {
            damage -= stat.defence;
        }
        else
        {
            return;
        }

        if (damage < 0)
            return;

        stat.curHP -= damage;

        if (stat.curHP < 1)
        {
            // �׾���
        }       
    }

    public void CalculateEXP(int exp)
    {
        stat.curEXP += exp;
        if (stat.curEXP >= stat.maxEXP)
        {
            float remnant = stat.curEXP - stat.maxEXP;
            stat.curEXP = 0;
            stat.curEXP += remnant;
            stat.maxEXP = stat.maxEXP * 1.2f;
        }
    }

    public bool CalculateCritical()
    {
        int random = Random.Range(1, 100);
        if (random > stat.criticalPercent)
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
            finalDamage = ((this.stat.damage * (stat.criticalDamage + 100)) / 100) - enemystat.statData.defence;
        }
        else
        {
            finalDamage = this.stat.damage - enemystat.statData.defence;
        }

        if (finalDamage <= 0)
            return 0;

        return finalDamage;
    }
}
