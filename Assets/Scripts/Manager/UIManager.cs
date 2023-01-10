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

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private SkillWindowUI skillUI;
    [SerializeField]
    private EquipmentUI equipmentUI;
    [SerializeField]
    private Image inventoryUI;
    [SerializeField]
    private InventoryUI equipmentInvenUi;
    [SerializeField]
    private InventoryUI usableInvenUI;
    [SerializeField]
    private InventoryUI materialInvenUI;

    private UIState curUI;

    private int cursorStack = 0;

    private void Update()
    {
        CurSorCheck();
        SkillUI();
        EquipmentUI();
        InventoryUI();
    }

    private void CurSorCheck()
    {
        if (cursorStack <= 0)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (cursorStack > 0)
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void SkillUI()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (skillUI.gameObject.activeSelf == true)
            {
                skillUI.gameObject.SetActive(false);
                cursorStack--;
            }
            else
            {
                skillUI.UpdateUI();
                skillUI.gameObject.SetActive(true);
                cursorStack++;
            }
        }
    }

    public void EquipmentUI()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (equipmentUI.gameObject.activeSelf)
            {
                equipmentUI.gameObject.SetActive(false);
                cursorStack--;
            }
            else
            {
                equipmentUI.gameObject.SetActive(true);
                cursorStack++;
            }
        }
    }

    public void InventoryUI()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryUI.gameObject.activeSelf)
            {
                Cursor.lockState = CursorLockMode.Locked;
                inventoryUI.gameObject.SetActive(false);
                cursorStack--;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                inventoryUI.gameObject.SetActive(true);
                cursorStack++;
            }
        }


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
