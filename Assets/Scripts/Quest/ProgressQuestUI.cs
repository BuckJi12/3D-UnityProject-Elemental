using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressQuestUI : MonoBehaviour
{
    private Image image;
    private TextMeshProUGUI[] texts;

    private void Awake()
    {
        texts = GetComponentsInChildren<TextMeshProUGUI>();
        image = GetComponent<Image>();
    }

    private void Start()
    {
        UnSet();
    }

    public void Set()
    {
        if (QuestManager.Instance.progressQuests[transform.GetSiblingIndex()].questData != null)
        {
            image.enabled = true;
            texts[0].enabled = true;
            texts[1].enabled = true;
            texts[0].text = QuestManager.Instance.progressQuests[transform.GetSiblingIndex()].questData.questcore;
            texts[1].text = QuestManager.Instance.progressQuests[transform.GetSiblingIndex()].curCount.ToString() + " / " +
                QuestManager.Instance.progressQuests[transform.GetSiblingIndex()].questData.goalCount;
        }
    }
    public void UnSet()
    {
        image.enabled = false;
        texts[0].enabled = false;
        texts[1].enabled = false;
    }
}
