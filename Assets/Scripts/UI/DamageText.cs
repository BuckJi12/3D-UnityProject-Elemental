using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    private Animator anim;
    private TextMeshProUGUI text;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }
    private void Update()
    {
        transform.LookAt(Camera.main.transform);
        transform.rotation = Camera.main.transform.rotation;
    }

    public void SetText(int damage, bool critical, Elemental type)
    {
        if (critical)
        {
            text.fontSize = 0.5f;
        }
        else
        {
            text.fontSize = 0.3f;
        }

        if (type == Elemental.Ice)
        {
            text.color = new Color32(133, 245, 242, 255);
        }
        else if (type == Elemental.Fire)
        {
            text.color = new Color32(255, 85, 32, 255);
        }
        else
        {
            text.color = new Color32(255, 255, 255, 255);
        }
        

        text.text = damage.ToString();
    }

    public void ReActionText(string text, byte r, byte g, byte b, float size)
    {
        this.text.color = new Color32(r, g, b, 255);
        this.text.fontSize = size;
        this.text.text = text;
    }
}
