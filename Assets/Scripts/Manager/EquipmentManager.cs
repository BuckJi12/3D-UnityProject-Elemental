using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : SingleTon<EquipmentManager>
{
    public Dictionary<EquipmentKind, InventoryItem> equips;

    [SerializeField]
    private EquipmentUI ui;

    public bool isWeaponEquip { get; private set; }

    private void Awake()
    {
        equips = new Dictionary<EquipmentKind, InventoryItem>();
        isWeaponEquip = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
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
    }

    public void Equip(InventoryItem item)
    {
        if (equips.TryGetValue(item.data.equipKind, out InventoryItem oldItem))
        {
            UnEquip(oldItem);
        }

        equips.Add(item.data.equipKind, item);

        if (item.data.equipKind == EquipmentKind.Weapon)
            isWeaponEquip = true;
    }

    public void UnEquip(InventoryItem item)
    {
        if (item.data.equipKind == EquipmentKind.Weapon)
            isWeaponEquip = false;

        InventoryManager.Instance.AddItem(item);
        equips.Remove(item.data.equipKind);
    }
}