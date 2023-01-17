using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Quest : ScriptableObject
{
    public int questID;
    public string questName;
    public string questDescription;
    public string questcore;

    public int questMoney;
    public List<QuestReward> rewards;

    public bool canComplete = false;

    public struct QuestReward
    {
        public GameObject prefab;
        public int count;
    }

    public virtual void PickUp() { }

    public virtual void Progress(Enemy enemy) { }

    public abstract void Check();
}
