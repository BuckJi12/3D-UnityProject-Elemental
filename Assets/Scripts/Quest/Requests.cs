using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Requests : MonoBehaviour
{
    private RequestPaper[] papers;

    private void Start()
    {
        SettingQuest();
    }

    public void SettingQuest()
    {
        papers = GetComponentsInChildren<RequestPaper>();
        for (int i = 0; i < papers.Length; i++)
        {
            papers[i].Set(RandomQuest());
        }
    }

    public Quest RandomQuest()
    {
        int random = Random.Range(1000, QuestManager.Instance.quests.Count + 1000);
        return QuestManager.Instance.quests[random];
    }

    public void Reroll()
    {
        SettingQuest();
    }
}
