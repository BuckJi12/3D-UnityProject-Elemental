using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Item : MonoBehaviour
{
    public ItemData data;

    public void Pick(PlayerColliders collider)
    {
        InventoryManager.Instance.AddItem(this);
        PoolManager.Instance.Release(this.gameObject);
    }
}
