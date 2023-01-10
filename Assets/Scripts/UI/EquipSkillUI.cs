using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipSkillUI : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    [SerializeField]
    private Image skillIcon;

    private Skill skill;

    private void Start()
    {
        skill = null;
        skillIcon.sprite = null;
        skillIcon.enabled = false;
    }

    public void UpdateUI()
    {
        if (SkillManager.Instance.equipSkill[transform.GetSiblingIndex()] == null)
        {
            this.skill = null;
            skillIcon.sprite = null;
            skillIcon.enabled = false;
        }
        else
        {
            skillIcon.enabled = true;
            this.skill = SkillManager.Instance.equipSkill[transform.GetSiblingIndex()];
            skillIcon.sprite = SkillManager.Instance.equipSkill[transform.GetSiblingIndex()].data.skillIcon;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        SkillSlot skillSlot = eventData.pointerDrag.GetComponent<SkillSlot>();
        SkillManager.Instance.EquipSkill(transform.GetSiblingIndex(), skillSlot.skill);
        UpdateUI();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (skill == null)
                return;

            SkillManager.Instance.OutSkill(transform.GetSiblingIndex());
            UpdateUI();
        }
    }
}
