using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


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

    public List<InventoryItem> equiments;
    public List<InventoryItem> usables;
    public List<InventoryItem> materials;


    private void Awake()
    {
        equiments = new List<InventoryItem>();
        usables = new List<InventoryItem>();
        materials = new List<InventoryItem>();
    }

    public void AddItem(InventoryItem inventoryItem)
    {
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
            equipmentUI.UpdateUI(UIState.Equipment);
        }
        else if (inventoryItem.data.kind == ItemKind.UsableItem)
        {
            usables.Remove(inventoryItem);
            usableUI.UpdateUI(UIState.Usable);
        }
        else
        {
            materials.Remove(inventoryItem);
            materialUI.UpdateUI(UIState.Material);
        }
    }

    public int FindItem(ItemData item)
    {
        if (item.kind == ItemKind.Equipment)
        {
            if (equiments.Find(x => x.data.name == item.name) == null)
                return 0;

            return equiments.Find(x => x.data.name == item.name).data.count;
        }
        else if (item.kind == ItemKind.UsableItem)
        {
            if (usables.Find(x => x.data.name == item.name) == null)
                return 0;

            return usables.Find(x => x.data.name == item.name).data.count;
        }
        else
        {
            if (materials.Find(x => x.data.name == item.name) == null)
                return 0;

            return materials.Find(x => x.data.name == item.name).data.count;
        }
    }
}

