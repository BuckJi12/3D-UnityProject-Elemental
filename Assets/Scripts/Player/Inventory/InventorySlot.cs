using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    [SerializeField]
    private Image itemIcon;

    private InventoryItem inventoryItem;


    public void OnItem(InventoryItem inventoryItem)
    {
        itemIcon.gameObject.SetActive(true);
        this.inventoryItem = inventoryItem;
        itemIcon.sprite = inventoryItem.data.itemIcon;
        itemIcon.enabled = true;
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
            
            if (draggableItem == null)
                return;
        draggableItem.afterDraw = transform;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (inventoryItem == null)
                return;

            inventoryItem.Use();
        }
    }
}
