using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RequestPaper : MonoBehaviour
{
    private Image paper;

    private Quest quest;

    private TextMeshProUGUI[] texts;


    private void Awake()
    {
        texts = GetComponentsInChildren<TextMeshProUGUI>();
    }

    public void Set(Quest quest)
    {
        this.gameObject.SetActive(true);
        this.quest = quest;
        texts[0].text = quest.questName;
        texts[1].text = quest.questDescription;
        texts[2].text = quest.questcore;
        texts[3].text = "º¸»ó\n" + quest.questMoney.ToString() + " ¿ø";
    }

    public void AcceptQuest()
    {
        if (quest == null)
            return;

        if (QuestManager.Instance.progressQuests.Count > 4)
            return;

        QuestManager.Instance.AddQuest(quest);
        this.gameObject.SetActive(false);
    }
}
