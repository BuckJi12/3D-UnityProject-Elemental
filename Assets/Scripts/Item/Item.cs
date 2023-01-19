using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Item : MonoBehaviour
{
    [HideInInspector]
    public ItemData data;

    public void Pick(PlayerColliders collider)
    {
        InventoryManager.Instance.AddItem(this);
        Destroy(gameObject);
    }
}
