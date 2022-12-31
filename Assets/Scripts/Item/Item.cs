using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData data;

    public void Pick(PlayerColliders collider)
    {
        InventoryManager.Instance.AddItem(this);

        Destroy(gameObject);
    }
}
