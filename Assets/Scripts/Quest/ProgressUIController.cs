using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressUIController : MonoBehaviour
{
    private ProgressQuestUI[] questUI;


    public void UpdateUI()
    {
        questUI = GetComponentsInChildren<ProgressQuestUI>();
        for (int i = 0; i < QuestManager.Instance.progressQuests.Capacity; i++)
        {
            if (i < QuestManager.Instance.progressQuests.Count)
            {
                questUI[i].Set();
            }
            else
            {
                questUI[i].UnSet();
            }
        }
    }
}
