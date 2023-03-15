using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CompleteUI : MonoBehaviour
{
    private CompleteQuest[] quests;

    private void Awake()
    {
        quests = GetComponentsInChildren<CompleteQuest>();
    }

    private void OnEnable()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        quests = GetComponentsInChildren<CompleteQuest>();
        for (int i = 0; i < 5; i++)
        {
            if (i < QuestManager.Instance.progressQuests.Count)
                quests[i].Set();
            else
                quests[i].UnSet();
        }
    }
}
