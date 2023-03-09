using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUnit : MonoBehaviour
{
    [SerializeField]
    private string uiName;

    private void Start()
    {
        UIManager.Instance.uis.Add(uiName, this);
        this.gameObject.SetActive(false);
    }
}
