using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillWindowUI : MonoBehaviour
{
    [HideInInspector]
    public SkillSlot[] skillSlots;

    private void Awake()
    {
        skillSlots = GetComponentsInChildren<SkillSlot>();
    }

    private void Start()
    {
        UpdateUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            UpdateUI();
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void UpdateUI()
    {
        skillSlots = GetComponentsInChildren<SkillSlot>();
        for (int i = 0; i < SkillManager.Instance.haveSkill.Count; i++)
        {
            skillSlots[i].OnSkill(SkillManager.Instance.haveSkill[i]);
            Debug.Log(i);
        }
    }
}
