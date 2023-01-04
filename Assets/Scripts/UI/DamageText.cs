using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    private Animator anim;
    private TextMeshPro text;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        transform.LookAt(Camera.main.transform);   
    }

    public void SetText(int damage)
    {
        text.text = damage.ToString();
    }
}
