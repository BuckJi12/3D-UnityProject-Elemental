using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyEventer : MonoBehaviour
{
    public UnityEvent enemyAttack;
    public UnityEvent enemySkill;


    public void OnAttack()
    {
        enemyAttack?.Invoke();
    }

    public void OnSkill()
    {
        enemySkill?.Invoke();
    }
}
