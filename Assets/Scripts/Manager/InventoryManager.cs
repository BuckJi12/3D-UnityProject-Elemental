using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;


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
    
    public List<InventoryItem> equipments = new List<InventoryItem>(25);
    public List<InventoryItem> usables = new List<InventoryItem>(25);
    public List<InventoryItem> materials = new List<InventoryItem>(25);


    private void Awake()
    { 
        Init();
    }

    public void Init()
    {
        for(int i = 0; i < equipments.Capacity; i++)
        {
            equipments.Add(null);
        }
        for (int i = 0; i < usables.Capacity; i++)
        {
            usables.Add(null);
        }
        for (int i = 0; i < usables.Capacity; i++)
        {
            materials.Add(null);
        }
    }

    public List<InventoryItem> ListType(InventoryItem inventoryItem)
    {
        if (inventoryItem.data.kind == ItemKind.Equipment)
        {
            return equipments;
        }
        else if (inventoryItem.data.kind == ItemKind.UsableItem)
        {
            return usables;
        }
        else
        {
            return materials;
        }
    }
    
    public InventoryUI UIType(InventoryItem inventoryItem)
    {
        if (inventoryItem.data.kind == ItemKind.Equipment)
        {
            return equipmentUI;
        }
        else if (inventoryItem.data.kind == ItemKind.UsableItem)
        {
            return usableUI;
        }
        else
        {
            return materialUI;
        }
    }

    public UIState GetUIState(InventoryItem inventoryItem)
    {
        if (inventoryItem.data.kind == ItemKind.Equipment)
        {
            return global::UIState.Equipment;
        }
        else if (inventoryItem.data.kind == ItemKind.UsableItem)
        {
            return global::UIState.Usable;
        }
        else
        {
            return global::UIState.Material;
        }
    }

    public void AddItem(InventoryItem inventoryItem, int count = 1)
    {
        if (inventoryItem.data.kind != ItemKind.Equipment)
        {
            for (int i = 0; i < ListType(inventoryItem).Capacity; i++)
            {
                if (ListType(inventoryItem)[i] != null)
                {
                    if (ListType(inventoryItem)[i].data.name == inventoryItem.data.name)
                    {
                        ListType(inventoryItem)[i].count += count;
                        UIType(inventoryItem).UpdateUI(GetUIState(inventoryItem));
                        return;
                    }
                }
            }
        }

        for (int i = 0; i < ListType(inventoryItem).Capacity; i++)
        {
            if (ListType(inventoryItem)[i] != null)
            {
                continue;
            }
            else
            {
                ListType(inventoryItem)[i] = inventoryItem;
                ListType(inventoryItem)[i].count = 1;
                UIType(inventoryItem).UpdateUI(GetUIState(inventoryItem));
                return;
            }
        }

    }

    public void AddItem(Item item, int count = 1)
    {
        InventoryItem inventoryItem = new InventoryItem();
        inventoryItem.data = item.data;

        if (inventoryItem.data.kind != ItemKind.Equipment)
        {
            for(int i = 0; i < ListType(inventoryItem).Capacity; i++)
            {
                if (ListType(inventoryItem)[i] != null)
                {
                    if (ListType(inventoryItem)[i].data.name == inventoryItem.data.name)
                    {
                        ListType(inventoryItem)[i].count += count;
                        UIType(inventoryItem).UpdateUI(GetUIState(inventoryItem));
                        return;
                    }
                }
            }
        }

        for (int i = 0; i < ListType(inventoryItem).Capacity; i++)
        {
            if (ListType(inventoryItem)[i] != null)
            {
                continue;
            }
            else
            {
                ListType(inventoryItem)[i] = inventoryItem;
                ListType(inventoryItem)[i].count = 1;
                UIType(inventoryItem).UpdateUI(GetUIState(inventoryItem));
                return;
            }
        }

    }

    public void InsertItem(InventoryItem inventoryItem, int index)
    {
        if (inventoryItem.data.kind == ItemKind.Equipment)
        {
            equipments[index] = inventoryItem;
        }
        else if (inventoryItem.data.kind == ItemKind.UsableItem)
        {
            usables[index] = inventoryItem;
        }
        else
        {
            materials[index] = inventoryItem;
        }
    }

    public void RemoveItem(InventoryItem inventoryItem)
    {
        int index = ListType(inventoryItem).FindIndex(x => x == inventoryItem);
        if (ListType(inventoryItem)[index].count > 1)
        {
            ListType(inventoryItem)[index].count -= 1;
        }
        else
        {
            ListType(inventoryItem)[index] = null;
        }
        UIType(inventoryItem).UpdateUI(GetUIState(inventoryItem));
    }

    public int FindItem(ItemData item)
    {
        if (item.kind == ItemKind.Equipment)
        {
            if (equipments.Find(x => x.data == item) != null)
                return 0;

            return equipments.Find(x => x.data == item).count;
        }
        else if (item.kind == ItemKind.UsableItem)
        {
            if (usables.Find(x => x.data == item) != null)
                return 0;

            return usables.Find(x => x.data == item).count;
        }
        else
        {
            var itemData = materials.FindIndex(x => x.data == item);
            if (itemData == -1)
                return 0;

            return materials.Find(x => x.data == item).count;
        }
    }

    public void Swap(InventoryItem inventoryItem, int index1, int index2)
    {
        InventoryItem temp = ListType(inventoryItem)[index1];
        ListType(inventoryItem)[index1] = ListType(inventoryItem)[index2];
        ListType(inventoryItem)[index2] = temp;
    }
}

