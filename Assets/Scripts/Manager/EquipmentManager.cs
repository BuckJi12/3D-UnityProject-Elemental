using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EquipmentManager : SingleTon<EquipmentManager>
{
    public Dictionary<EquipmentKind, InventoryItem> equips;

    [SerializeField]
    private EquipmentUI ui;

    private Stat equipStat;

    public UnityEvent updateUI;
    public UnityEvent changeForm;

    public bool isWeaponEquip { get; private set; }

    private void Awake()
    {
        equips = new Dictionary<EquipmentKind, InventoryItem>();
        isWeaponEquip = false;
    }

    public void Equip(InventoryItem item)
    {
        if (equips.TryGetValue(item.data.equipKind, out InventoryItem oldItem))
        {
            UnEquip(oldItem);
        }

        equips.Add(item.data.equipKind, item);
        AddStat(item);

        if (item.data.equipKind == EquipmentKind.Weapon)
        {
            ResourceManager.Instance.TakeResource(item);
            isWeaponEquip = true;
            changeForm?.Invoke();
        }

        updateUI?.Invoke();
    }

    public void UnEquip(InventoryItem item)
    {
        if (item.data.equipKind == EquipmentKind.Weapon)
        {
            isWeaponEquip = false;
            ResourceManager.Instance.RemoveResource(item);
            changeForm?.Invoke();
        }

        InventoryManager.Instance.AddItem(item);
        equips.Remove(item.data.equipKind);
        MinusStat(item);

        updateUI?.Invoke();
    }

    public void AddStat(InventoryItem item)
    {
        if (item.data.equipKind == EquipmentKind.Weapon)
        {
            WeaponData weapondata = item.data as WeaponData;
            PlayerStatManager.Instance.stat.damage += weapondata.damage;
            PlayerStatManager.Instance.stat.elementalPower += weapondata.elementalPower;
            PlayerStatManager.Instance.stat.criticalPercent += weapondata.criticalPercent;
            PlayerStatManager.Instance.stat.criticalDamage += weapondata.criticalDamage;
            
        }
        else if (item.data.equipKind == EquipmentKind.Accessory)
        {

        }
        else
        {
            
        }
    }
    
    public void MinusStat(InventoryItem item)
    {
        if (item.data.equipKind == EquipmentKind.Weapon)
        {
            WeaponData weapondata = item.data as WeaponData;
            PlayerStatManager.Instance.stat.damage -= weapondata.damage;
            PlayerStatManager.Instance.stat.elementalPower -= weapondata.elementalPower;
            PlayerStatManager.Instance.stat.criticalPercent -= weapondata.criticalPercent;
            PlayerStatManager.Instance.stat.criticalDamage -= weapondata.criticalDamage;

        }
        else if (item.data.equipKind == EquipmentKind.Accessory)
        {

        }
        else
        {

        }
    }
}