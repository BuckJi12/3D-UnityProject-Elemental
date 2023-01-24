using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatUI : MonoBehaviour
{
    [SerializeField]
    private Image hp;
    [SerializeField]
    private TextMeshProUGUI hpValue;
    [SerializeField]
    private Image stamina;
    [SerializeField]
    private TextMeshProUGUI staminaValue;
    [SerializeField]
    private Image exp;
    [SerializeField]
    private TextMeshProUGUI expValue;
    [SerializeField]
    private TextMeshProUGUI level;
    [SerializeField]
    private TextMeshProUGUI money;

    private void Update()
    {
        hp.fillAmount = (float)PlayerStatManager.Instance.stat.curHP / (float)PlayerStatManager.Instance.stat.maxHP;
        stamina.fillAmount = (float)PlayerStatManager.Instance.stat.curStamina / (float)PlayerStatManager.Instance.stat.maxStamina;
        exp.fillAmount = (float)PlayerStatManager.Instance.stat.curEXP / (float)PlayerStatManager.Instance.stat.maxEXP;

        hpValue.text = PlayerStatManager.Instance.stat.curHP.ToString() + " / " + PlayerStatManager.Instance.stat.maxHP.ToString();
        staminaValue.text = PlayerStatManager.Instance.stat.curStamina.ToString() + " / " + PlayerStatManager.Instance.stat.maxStamina.ToString();
        expValue.text = PlayerStatManager.Instance.stat.curEXP.ToString() + " / " + PlayerStatManager.Instance.stat.maxEXP.ToString();
        level.text = "LV " + PlayerStatManager.Instance.level.ToString();
        money.text = GameManager.Instance.money.ToString();
    }
}
