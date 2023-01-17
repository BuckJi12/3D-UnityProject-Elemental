using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Quest : MonoBehaviour
{
    public int questID;
    public string questName;
    public string questDescription;
    public string questcore;

    public int questMoney;
    public List<QuestReward> rewards;

    public bool canComplete;

    public struct QuestReward
    {
        public GameObject prefab;
        public int count;
    }

    public virtual void Progress() { }

    public abstract void Check();
}
