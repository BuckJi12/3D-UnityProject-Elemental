using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : SingleTon<ResourceManager>
{
    public Transform weaponPos;
    public void TakeResource(InventoryItem item)
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/" + item.data.prefab.name);
        if (prefab == null)
            return;

        GameObject gameObject = Instantiate(prefab, weaponPos);
        gameObject.gameObject.name = item.data.prefab.name;
    }

    public void RemoveResource(InventoryItem item)
    {
        GameObject removeItem = weaponPos.transform.Find(item.data.prefab.name).gameObject;

        Destroy(removeItem);
    }

}
