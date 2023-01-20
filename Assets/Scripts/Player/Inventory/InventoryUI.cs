using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [HideInInspector]
    public InventorySlot[] inventorySlots;

    private void Awake()
    {
        inventorySlots = GetComponentsInChildren<InventorySlot>();
    }
    public void UpdateUI(UIState state)
    {
        inventorySlots = GetComponentsInChildren<InventorySlot>();
        if (state == UIState.Equipment)
        {
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                if (InventoryManager.Instance.equipments[i] != null)
                {
                    inventorySlots[i].OnItem(InventoryManager.Instance.equipments[i]);
                }
                else
                {
                    inventorySlots[i].NoItem();
                }
            }
        }

        else if (state == UIState.Usable)
        {
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                if (InventoryManager.Instance.usables[i] != null)
                {
                    inventorySlots[i].OnItem(InventoryManager.Instance.usables[i]);
                }
                else
                {
                    inventorySlots[i].NoItem();
                }
            }
        }

        else
        {
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                if (InventoryManager.Instance.materials[i] != null)
                {
                    inventorySlots[i].OnItem(InventoryManager.Instance.materials[i]);
                }
                else
                {
                    inventorySlots[i].NoItem();
                }
            }
        }
    }
}
