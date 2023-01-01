using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Item : MonoBehaviour
{
    public ItemData data;

    public void Pick(PlayerColliders collider)
    {
        InventoryItem inventoryItem = new InventoryItem();
        inventoryItem.data = data;
        InventoryManager.Instance.AddItem(inventoryItem);
        Destroy(gameObject);
    }
}
