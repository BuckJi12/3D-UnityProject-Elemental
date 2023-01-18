using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestManager : SingleTon<QuestManager>
{
    public Dictionary<int, Quest> quests;

    private void Awake()
    {
        quests = new Dictionary<int, Quest>();
    }

    public void AddQuest(Quest quest)
    {
        if (quests.Count >= 5)
            return;

        quests.Add(quest.questID, quest);
    }

    public void KillRequestUpdate(Enemy enemy)
    {
        for (int i = 0; i < quests.Count; i++)
        {
            if (quests[1000].type == QuestType.Kill)
                quests[1000].Kill(enemy);
            else
                continue;
        }
    }

    public void ItemRequestUpdate()
    {
        for (int i = 0; i < quests.Count; i++)
        {
            if (quests[i].type == QuestType.Collected)
                quests[i].PickUp();
            else
                continue;
        }
    }

    public void RemoveQuest(Quest quest)
    {
        quests.Remove(quest.questID);
    }
}
