using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestManager : SingleTon<QuestManager>
{
    public Dictionary<int, Quest> quests;
    public ProgressQuest[] progressQuests;

    private void Awake()
    {
        quests = new Dictionary<int, Quest>();
        progressQuests = new ProgressQuest[5];
    }

    public void AddQuest(Quest quest)
    {
        for(int i = 0; i < progressQuests.Length; i++)
        {
            if (progressQuests[i].questData != null)
                continue;
            else
            {
                progressQuests[i].questData = quest;
                progressQuests[i].curCount = 0;
                progressQuests[i].canComplete = false;
                return;
            }
        }
    }

    public void KillRequestUpdate(Enemy enemy)
    {
        for (int i = 0; i < progressQuests.Length; i++)
        {
            if (progressQuests[i].questData == null)
                continue;

            if (progressQuests[i].questData.type == QuestType.Kill)
            {
                if (progressQuests[i].questData.GetTargetData().name == enemy.data.name)
                {
                    progressQuests[i].curCount += 1;
                    if (progressQuests[i].curCount >= progressQuests[i].questData.goalCount)
                    {
                        progressQuests[i].canComplete = true;
                    }
                }
            }
        }
    }

    public void CollectRequestUpdate(Enemy enemy)
    {
        for (int i = 0; i < progressQuests.Length; i++)
        {
            if (progressQuests[i].questData == null)
                continue;

            if (progressQuests[i].questData.type == QuestType.Collected)
            {
                progressQuests[i].curCount = InventoryManager.Instance.FindItem(progressQuests[i].questData.GetItemData());
                if (progressQuests[i].curCount >= progressQuests[i].questData.goalCount)
                {
                    progressQuests[i].canComplete = true;
                }
                else
                {
                    progressQuests[i].canComplete = false;
                }
            }
        }
    }

    public void ReceiveReward()
    {
        for (int i = 0; i < progressQuests.Length; i++)
        {
            if (progressQuests[i].canComplete == true)
            {
                GameManager.Instance.money += progressQuests[i].questData.questMoney;
                if (progressQuests[i].questData.rewards != null)
                {
                    for (int j = 0; j < progressQuests[i].questData.rewards.Count; j++)
                    {
                        InventoryManager.Instance.AddItem(progressQuests[i].questData.rewards[j].prefab.GetComponent<Item>());
                    }
                }
                progressQuests[i].questData = null;
                progressQuests[i].curCount = 0;
                progressQuests[i].canComplete = false;
            }
            
        }
    }

    [Serializable]
    public struct ProgressQuest
    {
        public Quest questData;
        public int curCount;
        public bool canComplete;
    }
}
