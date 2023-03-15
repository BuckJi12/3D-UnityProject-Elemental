using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Image skillIcon;
    [SerializeField]
    private Image infoUI;

    [SerializeField]
    private Image infoIcon;
    [SerializeField]
    private TextMeshProUGUI title;
    [SerializeField]
    private TextMeshProUGUI description;
    [SerializeField]
    private TextMeshProUGUI type;
    [SerializeField]
    private TextMeshProUGUI damage;
    [SerializeField]
    private TextMeshProUGUI level;

    [HideInInspector]
    public Skill skill;

    private void Awake()
    {
        skillIcon = GetComponent<Image>();
        skillIcon.enabled = false;
    }

    public void OnSkill(Skill skill)
    {
        skillIcon.gameObject.SetActive(true);
        this.skill = skill;
        skillIcon.sprite = skill.data.skillIcon;
        skillIcon.enabled = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (skill != null)
        {
            infoUI.transform.position = Input.mousePosition + new Vector3(300, -300);
            infoUI.gameObject.SetActive(true);
            Debug.Log(infoIcon);
            infoIcon.sprite = skill.data.skillIcon;
            title.text = skill.data.name;
            description.text = "설명 : " + skill.data.description;
            type.text = "원소타입 : " + skill.data.type.ToString();
            damage.text = "데미지 : " + skill.data.damage.ToString() + "00%";
            level.text = "레벨 제한 : " + skill.data.canLevel.ToString() + "이상";
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        infoUI.gameObject.SetActive(false);
    }
}

