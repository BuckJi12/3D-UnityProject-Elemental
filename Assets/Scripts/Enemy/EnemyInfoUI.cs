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
    [SerializeField]
    private Image iceIcon;
    [SerializeField]
    private Image fireIcon;

    private EnemyStat enemyStat;

    private void Awake()
    {
        enemyStat = GetComponent<EnemyStat>();
    }

    private void Start()
    {
        Set();
        iceIcon.gameObject.SetActive(false);
        fireIcon.gameObject.SetActive(false);
    }
    private void Update()
    {
        nameAndLevel.transform.rotation = Camera.main.transform.rotation;
        healthBar.transform.rotation = Camera.main.transform.rotation;
        iceIcon.transform.rotation = Camera.main.transform.rotation;
        fireIcon.transform.rotation = Camera.main.transform.rotation;

        health.fillAmount = (float)enemyStat.curHP / (float)enemyStat.maxHP;
    }
    public void Set()
    {
        nameAndLevel.text = "LV  "+ enemyStat.statData.level.ToString() + " " + enemyStat.statData.name.ToString();
    }

    public void UpdateIcon()
    {
        switch (enemyStat.enemyState)
        {
            case Elemental.None:
                iceIcon.gameObject.SetActive(false);
                fireIcon.gameObject.SetActive(false);
                break;
            case Elemental.Ice:
                iceIcon.gameObject.SetActive(true);
                fireIcon.gameObject.SetActive(false);
                break;
            case Elemental.Fire:
                iceIcon.gameObject.SetActive(false);
                fireIcon.gameObject.SetActive(true);
                break;
            case Elemental.Lightning:
                break;
            default:
                break;
        }
    }
}
