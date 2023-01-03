using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyEventer : MonoBehaviour
{
    public UnityEvent enemyAttack;


    public void OnAttack()
    {
        enemyAttack?.Invoke();
    }
}
