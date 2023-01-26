using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CompleteQuest : MonoBehaviour
{
    private Image image;
    private TextMeshProUGUI[] texts;

    private void Awake()
    {
        image = GetComponent<Image>();
        texts = GetComponentsInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        UnSet();
    }

    public void Set()
    {
        image.enabled = true;
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].enabled = true;
        }
        texts[0].text = QuestManager.Instance.progressQuests[transform.GetSiblingIndex()].questData.questName;
        texts[1].text = QuestManager.Instance.progressQuests[transform.GetSiblingIndex()].questData.questDescription;
        texts[2].text = QuestManager.Instance.progressQuests[transform.GetSiblingIndex()].questData.questMoney.ToString() +"¿ø";
    }

    public void UnSet()
    {
        image.enabled = false;
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].enabled = false;
        }
    }
}
