using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RequestPaper : MonoBehaviour, IPointerClickHandler
{
    private Image paper;
    [SerializeField]
    private Quest quest;

    private TextMeshProUGUI[] texts;


    private void Awake()
    {
        texts = GetComponentsInChildren<TextMeshProUGUI>();
    }
    private void Start()
    {
        texts[0].text = quest.questName;
        texts[1].text = quest.questDescription;
        texts[2].text = quest.questcore;
        texts[3].text = "º¸»ó\n" + quest.questMoney.ToString() + " ¿ø";
    }

    public void AcceptQuest()
    {
        QuestManager.Instance.AddQuest(quest);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            AcceptQuest();
        }
    }
}
