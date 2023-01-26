using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class QuestManager : SingleTon<QuestManager>
{
    public Dictionary<int, Quest> quests;
    public List<ProgressQuest> progressQuests;

    [SerializeField]
    private List<Quest> listQuests;
    [SerializeField]
    private ProgressQuestUI[] questUIs;

    private void Awake()
    {
        quests = new Dictionary<int, Quest>();
        progressQuests = new List<ProgressQuest>(5);
        questUIs = new ProgressQuestUI[5];
        Init();
    }

    public void Init()
    {
        for (int i = 0; i < listQuests.Count; i++)
        {
            quests.Add(listQuests[i].questID, listQuests[i]);
        }
    }

    public void AddQuest(Quest quest)
    {
        if (progressQuests.Count > 4)
            return;

        ProgressQuest temp;
        temp.questData = quest;
        temp.curCount = 0;
        temp.canComplete = false;
        progressQuests.Add(temp);
        for (int i = 0; i < progressQuests.Capacity; i++)
        {
            if (progressQuests[i].questData == null)
                continue;

            questUIs[i].UpdateUI(progressQuests[i].questData, progressQuests[i].curCount);
        }
    }

    public void KillRequestUpdate(Enemy enemy)
    {
        for (int i = 0; i < progressQuests.Capacity; i++)
        {
            if (progressQuests[i].questData == null)
                continue;

            if (progressQuests[i].questData.type == QuestType.Kill)
            {
                if (progressQuests[i].questData.GetTargetData().name == enemy.data.name)
                {
                    ProgressQuest temp = progressQuests[i];
                    temp.curCount += 1;
                    if (progressQuests[i].curCount >= progressQuests[i].questData.goalCount)
                    {
                        temp.canComplete = true;
                    }
                    progressQuests[i] = temp;
                    questUIs[i].UpdateUI(progressQuests[i].questData, progressQuests[i].curCount);
                }
            }
        }
    }

    public void CollectRequestUpdate(Enemy enemy)
    {
        for (int i = 0; i < progressQuests.Capacity; i++)
        {
            if (progressQuests[i].questData == null)
                continue;

            if (progressQuests[i].questData.type == QuestType.Collected)
            {
                ProgressQuest temp = progressQuests[i];
                temp.curCount = InventoryManager.Instance.FindItem(progressQuests[i].questData.GetItemData());
                if (progressQuests[i].curCount >= progressQuests[i].questData.goalCount)
                {
                    temp.canComplete = true;
                }
                else
                {
                    temp.canComplete = false;
                }
                progressQuests[i] = temp;
                questUIs[i].UpdateUI(progressQuests[i].questData, progressQuests[i].curCount);
            }
        }
    }

    public void ReceiveReward()
    {
        for (int i = 0; i < progressQuests.Capacity; i++)
        {
            if (progressQuests[i].questData == null)
                continue;

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
                ProgressQuest temp = progressQuests[i];
                temp.questData = null;
                temp.curCount = 0;
                temp.canComplete = false;
                progressQuests[i] = temp;
                questUIs[i].UpdateUI(progressQuests[i].questData, progressQuests[i].curCount);
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
