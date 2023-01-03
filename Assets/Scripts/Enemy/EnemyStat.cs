using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    public MonsterData statData;
    public int curHP;
    public int maxHP;
    public Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }
    public void Die()
    {
        Destroy(gameObject);
    }
}
