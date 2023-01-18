using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : ScriptableObject
{
    public int questID;
    public string questName;
    public string questDescription;
    public string questcore;
    public QuestType type;
    public int goalCount;

    public int questMoney;
    public List<QuestReward> rewards;

    public bool canComplete = false;

    public struct QuestReward
    {
        public GameObject prefab;
        public int count;
    }

    public virtual MonsterData GetTargetData() { return null; }
    public virtual ItemData GetItemData() { return null; }
}
