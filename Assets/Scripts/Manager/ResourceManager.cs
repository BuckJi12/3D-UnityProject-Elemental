using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : SingleTon<ResourceManager>
{
    public Transform weaponPos;
    private GameObject curWeapon;
    public void TakeResource(InventoryItem item)
    {
        curWeapon = PoolManager.Instance.Get(Resources.Load<GameObject>("Prefabs/" + item.data.prefab.name));
        if (curWeapon == null)
            return;

        curWeapon.transform.SetParent(weaponPos);
        curWeapon.transform.localPosition = new Vector3(0, 0, 0);
        curWeapon.transform.localRotation = item.data.prefab.transform.rotation;
    }

    public void RemoveResource(InventoryItem item)
    {
        PoolManager.Instance.Release(curWeapon);
    }

}
