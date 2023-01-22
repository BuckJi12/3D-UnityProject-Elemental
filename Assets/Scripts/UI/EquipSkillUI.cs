using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipSkillUI : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    [SerializeField]
    private Image skillIcon;
    [SerializeField]
    private Image coolTimeUI;
    [SerializeField]
    private TextMeshProUGUI timer;


    private Skill skill;

    private void Start()
    {
        skill = null;
        skillIcon.sprite = null;
        skillIcon.enabled = false;
        coolTimeUI.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (skill == null)
            return;

        if (Input.GetButtonDown("Skill" + (transform.GetSiblingIndex() + 1)) && skill.canUse)
        {
            coolTimeUI.gameObject.SetActive(true);
            SkillManager.Instance.equipSkill[transform.GetSiblingIndex()].StartCoolTime();
            StartCoroutine(CoolTimeUI(skill.data.coolTime));
        }
    }

    public IEnumerator CoolTimeUI(float coolTime)
    {
        while (!skill.canUse)
        {
            timer.text = ((int)coolTime).ToString();
            coolTime -= Time.deltaTime;
            coolTimeUI.fillAmount = coolTime / skill.data.coolTime;
            yield return new WaitForFixedUpdate();
        }
        
        coolTimeUI.gameObject.SetActive(false);
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
        if (skillSlot == null)
            return;
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
