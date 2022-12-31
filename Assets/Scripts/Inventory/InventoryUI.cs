using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private InventorySlot[] inventoryUnits;

    public void UpdateUI(UIState state)
    {
        inventoryUnits = GetComponentsInChildren<InventorySlot>();
        if (state == UIState.Equipment)
        {
            for (int i = 0; i < inventoryUnits.Length; i++)
            {
                if (i < InventoryManager.Instance.equiments.Count)
                {
                    //inventoryUnits[i].AddItem(InventoryManager.Instance.equiments[i]);
                }
                else
                {
                    //inventoryUnits[i].RemoveItem();
                }
            }
        }

        else if (state == UIState.Usable)
        {
            for (int i = 0; i < inventoryUnits.Length; i++)
            {
                if (i < InventoryManager.Instance.usables.Count)
                {
                    //inventoryUnits[i].AddItem(InventoryManager.Instance.usables[i]);
                }
                else
                {
                    //inventoryUnits[i].RemoveItem();
                }
            }
        }

        else 
        {
            for (int i = 0; i < inventoryUnits.Length; i++)
            {
                if (i < InventoryManager.Instance.materials.Count)
                {
                    //inventoryUnits[i].AddItem(InventoryManager.Instance.materials[i]);
                }
                else
                {
                    //inventoryUnits[i].RemoveItem();
                }
            }
        }
    }
}
