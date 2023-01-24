using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStatManager : SingleTon<PlayerStatManager>
{
    public Stat stat;
    public int level = 1;

    [SerializeField]
    private GameObject levelUp;
    [SerializeField]
    private Canvas canvas;

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
            // ав╬З╢ы
        }       
    }

    private void Start()
    {
        StartCoroutine(RecoveryHP());
        StartCoroutine(RecoveryStamina());
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
            LevelUp();
        }
    }
    
    public void LevelUp()
    {
        stat.curHP += 50;
        stat.maxHP += 50;
        stat.curStamina += 10;
        stat.maxStamina += 10;
        stat.damage += 10;
        stat.elementalPower += 10;
        stat.defence += 2;
        level++;
        GameObject instance = PoolManager.Instance.Get(levelUp);
        instance.transform.SetParent(canvas.transform);
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

    public int CalculateDamage(Enemy enemy, bool critical)
    {
        int finalDamage;
        if (critical)
        {
            finalDamage = ((this.stat.damage * (stat.criticalDamage + 100)) / 100) - enemy.defence;
        }
        else
        {
            finalDamage = this.stat.damage - enemy.defence;
        }

        if (finalDamage <= 0)
            return 0;

        return finalDamage;
    }

    public int CalculateSkillDamage(Enemy enemy, Skill skill, bool critical)
    {
        int finalDamage;
        if (critical)
        {
            finalDamage = (((this.stat.elementalPower * skill.data.damage) * (stat.criticalDamage + 100)) / 100) - enemy.defence;
        }
        else
        {
            finalDamage = (this.stat.elementalPower * skill.data.damage) - enemy.defence;
        }

        if (finalDamage <= 0)
            return 0;

        return finalDamage;
    }

    public IEnumerator RecoveryHP()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);

            if (stat.curHP == stat.maxHP)
                yield return null;

            stat.curHP += (stat.maxHP / 100);
            if (stat.curHP > stat.maxHP)
                stat.curHP = stat.maxHP;
        }
    }

    public IEnumerator RecoveryStamina()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            if (stat.curStamina == stat.maxStamina)
                yield return null;


            stat.curStamina += 10;
            if (stat.curStamina > stat.maxStamina)
                stat.curStamina = stat.maxStamina;
        }
    }
}
