using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField]
    private Image itemIcon;

    public void OnItem(InventoryItem inventoryItem)
    {
        itemIcon.sprite = inventoryItem.data.itemIcon;
    }

    public void NoItem()
    {
        itemIcon.gameObject.SetActive(false);
    }
}
