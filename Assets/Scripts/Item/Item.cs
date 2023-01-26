using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Progress;

public class Item : MonoBehaviour
{
    public ItemData data;
    //public UnityEvent OnCollectUpdate;

    public void Pick(PlayerColliders collider)
    {
        //OnCollectUpdate?.Invoke();
        InventoryManager.Instance.AddItem(this);
        PoolManager.Instance.Release(this.gameObject);
    }
}
