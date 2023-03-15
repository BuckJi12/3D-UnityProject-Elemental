using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalReaction : MonoBehaviour
{
    [HideInInspector]
    public EnemyData data;
    [HideInInspector]
    public Elemental elementalState;

    private EnemyInfoUI infoUI;

    private void Awake()
    {
        data = GetComponent<EnemyData>();
        infoUI = GetComponent<EnemyInfoUI>();
    }

    private void Update()
    {
        Expansion();
    }

    public void Reaction(Skill skill)
    {
        switch (elementalState)
        {
            case Elemental.None:    // �Ϲ� ����
                // ��ų�� ���Ҹ� �ְ� �ش� ������ ������� �Ǵ�
                if (skill.data.type == Elemental.Ice)
                {
                    StartCoroutine(FrostBite());
                    elementalState = Elemental.Ice;
                    data.ReactionText("����", 133, 245, 242, 0.2f);
                    infoUI.UpdateIcon();
                }
                if (skill.data.type == Elemental.Fire)
                {
                    StartCoroutine(FireDotDamage());
                    elementalState = Elemental.Fire;
                    data.ReactionText("ȭ��", 255, 85, 32, 0.2f);
                    infoUI.UpdateIcon();
                }
                break;
            case Elemental.Ice:     // ���� ����
                // �Ϲ� ���� �� ������
                // ȭ�� ���� �� ���� & ȭ�� ���·� ����
                if (skill.data.type == Elemental.Fire)
                {
                    elementalState = (Elemental.Ice | Elemental.Fire);
                }
                break;
            case Elemental.Fire:    // ȭ�� ����
                // �Ϲ� ���ݽ� ������
                // ���� ���ݽ� ���� & ȭ�� ���·� ����
                if (skill.data.type == Elemental.Ice)
                {
                    elementalState = (Elemental.Ice | Elemental.Fire);
                }
                break;
            case Elemental.Lightning:   // ���� ���� ����
                break;
        }
    }

    public IEnumerator FireDotDamage()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(2);
            data.curHP -= (PlayerStatManager.Instance.stat.elementalPower / 100);
            data.SkillDamageText(PlayerStatManager.Instance.stat.elementalPower / 100, false, Elemental.Fire);

            // �ؽ�Ʈ ���
        }

        elementalState = Elemental.None;
        infoUI.UpdateIcon();
    }

    public IEnumerator FrostBite()
    {
        int originalDefence = data.defence;
        data.defence = (int)(data.defence * 0.7f);
        data.agent.speed = 1;


        yield return new WaitForSeconds(10);
        elementalState = Elemental.None;
        infoUI.UpdateIcon();
        data.defence = originalDefence;
        data.agent.speed = 2;
    }

    public void Expansion()
    {
        if ((elementalState & (Elemental.Ice | Elemental.Fire))
            == (Elemental.Ice | Elemental.Fire))
        {
            StopAllCoroutines();
            data.curHP -= (PlayerStatManager.Instance.stat.elementalPower * 10);
            data.SkillDamageText(PlayerStatManager.Instance.stat.elementalPower * 10, false, Elemental.Fire);
            elementalState = Elemental.None;
            data.ReactionText("��â", 225, 125, 85, 0.3f);
            infoUI.UpdateIcon();
        }
    }
}
