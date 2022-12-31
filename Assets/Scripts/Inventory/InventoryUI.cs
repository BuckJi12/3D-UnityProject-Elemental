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
                    inventoryUnits[i].OnItem(InventoryManager.Instance.equiments[i]);
                }
                else
                {
                    inventoryUnits[i].NoItem();
                }
            }
        }

        else if (state == UIState.Usable)
        {
            for (int i = 0; i < inventoryUnits.Length; i++)
            {
                if (i < InventoryManager.Instance.usables.Count)
                {
                    inventoryUnits[i].OnItem(InventoryManager.Instance.usables[i]);
                }
                else
                {
                    inventoryUnits[i].NoItem();
                }
            }
        }

        else 
        {
            for (int i = 0; i < inventoryUnits.Length; i++)
            {
                if (i < InventoryManager.Instance.materials.Count)
                {
                    inventoryUnits[i].OnItem(InventoryManager.Instance.materials[i]);
                }
                else
                {
                    inventoryUnits[i].NoItem();
                }
            }
        }
    }
}
