using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyInfoUI : MonoBehaviour
{
    [SerializeField]
    private Image health;
    [SerializeField]
    private TextMeshProUGUI nameAndLevel;
    [SerializeField]
    private Image healthBar;

    private EnemyStat enemyStat;

    private void Awake()
    {
        enemyStat = GetComponent<EnemyStat>();
    }

    private void Start()
    {
        Set();
    }
    private void Update()
    {
        nameAndLevel.transform.rotation = Camera.main.transform.rotation;
        healthBar.transform.rotation = Camera.main.transform.rotation;

        health.fillAmount = (float)enemyStat.curHP / (float)enemyStat.maxHP;
    }
    public void Set()
    {
        nameAndLevel.text = "LV  "+ enemyStat.statData.level.ToString() + " " + enemyStat.statData.name.ToString();
    }
}
