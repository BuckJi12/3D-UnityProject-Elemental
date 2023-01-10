using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    [SerializeField]
    private Image skillIcon;

    public Skill skill;

    private void Start()
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
}

