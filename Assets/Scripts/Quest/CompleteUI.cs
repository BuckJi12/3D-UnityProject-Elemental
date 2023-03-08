using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CompleteUI : MonoBehaviour
{
    private CompleteQuest[] quests;
    private GameObject parent;

    private void Awake()
    {
        quests = GetComponentsInChildren<CompleteQuest>();
        parent = GetComponentInParent<GameObject>();
    }

    private void Start()
    {
        parent.SetActive(false);
    }

    public void UpdateUI()
    {
        quests = GetComponentsInChildren<CompleteQuest>();
        for (int i = 0; i < QuestManager.Instance.progressQuests.Count; i++)
        {
            if (QuestManager.Instance.progressQuests[i].canComplete)
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
