using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem
{
    public ItemData data;

    public void Use()
    {
        if (!data.usable)
            return;
        
        if (data.kind == ItemKind.Equipment)
        {
            EquipmentManager.Instance.Equip(this);
            InventoryManager.Instance.RemoveItem(this);
        }

        else if (data.kind == ItemKind.UsableItem)
        {
            // ������ ���
        }
    }

    public void Drop()
    {

    }
}
