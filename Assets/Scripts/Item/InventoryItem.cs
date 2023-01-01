using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public ItemData data;

    public void Use()
    {
        if (!data.usable)
            return;
        
        if (data.kind == ItemKind.Equipment)
        {
            Debug.Log("전 발동함ㅋㅋ");
            EquipmentManager.Instance.Equip(this);
            InventoryManager.Instance.RemoveItem(this);
        }

        else if (data.kind == ItemKind.UsableItem)
        {
            // 아이템 사용
        }
    }

    public void Drop()
    {

    }
}
