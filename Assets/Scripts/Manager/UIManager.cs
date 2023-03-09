using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UIState
{
    Equipment,
    Usable,
    Material
}

public class UIManager : SingleTon<UIManager>
{
    [Header("Inventory")]
    [SerializeField]
    private InventoryUI equipmentInvenUi;
    [SerializeField]
    private InventoryUI usableInvenUI;
    [SerializeField]
    private InventoryUI materialInvenUI;

    private UIState curUI;

    public int cursorStack = 0;

    public Dictionary<string, UIUnit> uis;
    public List<UIUnit> uisList;

    public bool isUsingMouse;

    private void Awake()
    {
        uis = new Dictionary<string, UIUnit>();
        uisList = new List<UIUnit>();
        isUsingMouse = false;
    }

    private void Update()
    {
        OnPressAlt();
        UIRenewal();
        InventoryUI();
    }

    private void UIRenewal()
    {
        if (Input.GetKeyDown(KeyCode.K))
            SwitchUI("Skill");

        if (Input.GetKeyDown(KeyCode.I))
            SwitchUI("Inventory");

        if (Input.GetKeyDown(KeyCode.O))
            SwitchUI("Equipment");
    }

    private void OnPressAlt()
    {
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            Cursor.lockState = CursorLockMode.None;
            isUsingMouse = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            isUsingMouse = false;
        }
    }

    public void SwitchUI(string name)
    {
        if (uis[name].gameObject.activeSelf)
        {
            uis[name].gameObject.SetActive(false);
            uisList.Add(uis[name]);
        }
        else
        {
            uis[name].gameObject.SetActive(true);
            uisList.Remove(uis[name]);
        }
    }

    public void InventoryUI()
    {
        switch (curUI)
        {
            case UIState.Equipment:
                equipmentInvenUi.gameObject.SetActive(true);
                usableInvenUI.gameObject.SetActive(false);
                materialInvenUI.gameObject.SetActive(false);
                break;
            case UIState.Usable:
                equipmentInvenUi.gameObject.SetActive(false);
                usableInvenUI.gameObject.SetActive(true);
                materialInvenUI.gameObject.SetActive(false);
                break;
            case UIState.Material:
                equipmentInvenUi.gameObject.SetActive(false);
                usableInvenUI.gameObject.SetActive(false);
                materialInvenUI.gameObject.SetActive(true);
                break;
        }
    }

    public void ShowEquipment()
    {
        curUI = UIState.Equipment;
    }

    public void ShowUsable()
    {
        curUI = UIState.Usable;
    }

    public void ShowMaterial()
    {
        curUI = UIState.Material;
    }
}
