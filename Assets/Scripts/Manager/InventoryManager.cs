using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum UIState
{
    Equipment,
    Usable,
    Material
}
public class InventoryManager : SingleTon<InventoryManager>
{
    [SerializeField]
    private Image ui;
    [SerializeField]
    private InventoryUI equipmentUI;
    [SerializeField]
    private InventoryUI usableUI;
    [SerializeField]
    private InventoryUI materialUI;

    private UIState curUI;

    public List<InventoryItem> equiments;
    public List<InventoryItem> usables;
    public List<InventoryItem> materials;


    private void Awake()
    {
        equiments = new List<InventoryItem>();
        usables = new List<InventoryItem>();
        materials = new List<InventoryItem>();
        curUI = UIState.Equipment;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (ui.gameObject.activeSelf)
            {
                Cursor.lockState = CursorLockMode.Locked;
                ui.gameObject.SetActive(false);
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                ui.gameObject.SetActive(true);
            }
        }


        switch (curUI)
        {
            case UIState.Equipment:
                equipmentUI.gameObject.SetActive(true);
                usableUI.gameObject.SetActive(false);
                materialUI.gameObject.SetActive(false);
                break;
            case UIState.Usable:
                equipmentUI.gameObject.SetActive(false);
                usableUI.gameObject.SetActive(true);
                materialUI.gameObject.SetActive(false);
                break;
            case UIState.Material:
                equipmentUI.gameObject.SetActive(false);
                usableUI.gameObject.SetActive(false);
                materialUI.gameObject.SetActive(true);
                break;
        }
    }

    public void AddItem(Item item)
    {
        InventoryItem inventoryItem = new InventoryItem();
        inventoryItem.data = item.data;

        if (inventoryItem.data.kind == ItemKind.Equipment)
        {
            equiments.Add(inventoryItem);
            equipmentUI.UpdateUI(UIState.Equipment);
        }
        else if (inventoryItem.data.kind == ItemKind.UsableItem)
        {
            usables.Add(inventoryItem);
            usableUI.UpdateUI(UIState.Usable);
        }
        else 
        {
            materials.Add(inventoryItem);
            materialUI.UpdateUI(UIState.Material);
        }
    }

    public void RemoveItem(InventoryItem inventoryItem)
    {
        if (inventoryItem.data.kind == ItemKind.Equipment)
        {
            equiments.Remove(inventoryItem);
        }
        else if (inventoryItem.data.kind == ItemKind.UsableItem)
        {
            usables.Remove(inventoryItem);
        }
        else
        {
            materials.Remove(inventoryItem);
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

