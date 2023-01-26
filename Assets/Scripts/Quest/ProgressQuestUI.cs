using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProgressQuestUI : MonoBehaviour
{ 
    private TextMeshProUGUI[] texts;

    private void Awake()
    {
        texts = GetComponentsInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        this.gameObject.SetActive(false);
    }

    public void UpdateUI(Quest questData, int count)
    {
        if (QuestManager.Instance.progressQuests[transform.GetSiblingIndex()].questData != null)
        {
            this.gameObject.SetActive(true);
            texts[0].text = questData.questcore;
            texts[1].text = count.ToString() + " / " + questData.goalCount;
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
}
