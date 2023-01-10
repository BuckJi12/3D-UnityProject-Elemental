using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : SingleTon<SkillManager>
{
    public List<Skill> equipSkill;
    public List<Skill> haveSkill;

    [SerializeField]
    private EquipSkillUI q;
    [SerializeField]
    private EquipSkillUI e;
    [SerializeField]
    private EquipSkillUI r;

    private void Awake()
    {
        equipSkill = new List<Skill>();
        haveSkill = new List<Skill>();
    }

    private void Start()
    {
        equipSkill.Add(null);
        equipSkill.Add(null);
        equipSkill.Add(null);
    }

    public void EquipSkill(int index, Skill skill)
    {
        // �̹� �ִ� ��ų�� ������ �ϴ� �����
        if (equipSkill.Find(x => x == skill))
        {
            int temp = equipSkill.FindIndex(x => x == skill);
            equipSkill[temp] = null;
            UIUpdate();
        }


        // index�� ��ų�� �ִ´�
        if (equipSkill[index] == null)
        {
            equipSkill[index] = skill;
            return;
        }

        // index�� �̹� �־����� ����ΰ� �ִ´�
        equipSkill[index] = null;
        equipSkill[index] = skill;
    }

    public void OutSkill(int index)
    {
        equipSkill[index] = null;
    }

    public void UIUpdate()
    {
        q.UpdateUI();
        e.UpdateUI();
        r.UpdateUI();
    }
}
