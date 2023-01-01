using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    [SerializeField]
    private Image itemIcon;

    public void OnItem(InventoryItem inventoryItem)
    {
        itemIcon.gameObject.SetActive(true);
        itemIcon.sprite = inventoryItem.data.itemIcon;
    }

    public void NoItem()
    {
        itemIcon.sprite = null;
        itemIcon.enabled = false;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount <= 1)
        {
        GameObject dropItem = eventData.pointerDrag;
        DraggableItem draggableItem = dropItem.GetComponent<DraggableItem>();
        draggableItem.afterDraw = transform;
        }
    }
}
