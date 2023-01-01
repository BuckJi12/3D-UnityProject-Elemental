using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EquipmentUI : MonoBehaviour
{
    [HideInInspector]
    public EquipmentSlot[] equipmentSlots;

    private void Awake()
    {
        equipmentSlots = GetComponentsInChildren<EquipmentSlot>();
    }

    public void UpdateUI()
    {
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            if (EquipmentManager.Instance.equips.TryGetValue((EquipmentKind)i , out InventoryItem item))
            {
                equipmentSlots[i].OnItem(item);
            }
            else
            {
                equipmentSlots[i].NoItem();
            }
        }
    }
}
