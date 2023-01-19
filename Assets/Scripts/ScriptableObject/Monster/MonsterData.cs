using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Monster/MonsterData")]
public class MonsterData : ScriptableObject
{
    public new string name;
    public int level;

    public int exp;
    public int money;

    [SerializeField]
    public List<DropItem> dropItems;

    [Serializable]
    public struct DropItem
    {
        public ItemData itemData;
        public int dropRate;
    }
}
