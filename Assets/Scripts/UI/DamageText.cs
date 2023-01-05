using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    private Animator anim;
    private TextMeshProUGUI text;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        text = GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        transform.LookAt(Camera.main.transform);
        transform.rotation = Camera.main.transform.rotation;
    }

    public void SetText(int damage)
    {
        text.text = damage.ToString();
    }
}
