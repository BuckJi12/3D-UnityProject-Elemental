using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Monster/MonsterData")]
public class MonsterData : ScriptableObject
{
    public new string name;
    public int level;

    public int damage;

    public int defence;

    public int exp;

    public List<GameObject> dropItems;
}
