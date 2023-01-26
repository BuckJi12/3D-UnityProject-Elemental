using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CompleteUI : MonoBehaviour
{
    private CompleteQuest[] quests;
    
    public void UpdateUI()
    {
        quests = GetComponentsInChildren<CompleteQuest>();
        for (int i = 0; i < QuestManager.Instance.progressQuests.Capacity; i++)
        {
            if (i < QuestManager.Instance.progressQuests.Count)
            {
                quests[i].Set();
            }
            else
            {
                quests[i].UnSet();
            }
        }
    }
}
