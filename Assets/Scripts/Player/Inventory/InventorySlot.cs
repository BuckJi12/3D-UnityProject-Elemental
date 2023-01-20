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
        inventoryItem = null;
    }

    public void OnDrop(PointerEventData eventData)
    {
        DraggableItem dragItem = eventData.pointerDrag.GetComponent<DraggableItem>();
        InventorySlot dropItem = dragItem.parent.GetComponent<InventorySlot>();
        //InventorySlot dropItem = eventData.pointerDrag.GetComponentInParent<InventorySlot>();
        if (dropItem == null)
            return;

        //if (inventoryItem == null)
        //{
        //    InventoryManager.Instance.InsertItem(dropItem.inventoryItem, transform.GetSiblingIndex());
        //    InventoryManager.Instance.ListType(dropItem.inventoryItem)[dropItem.transform.GetSiblingIndex()] = null;
        //    InventoryManager.Instance.UIType(dropItem.inventoryItem).UpdateUI(InventoryManager.Instance.GetUIState(dropItem.inventoryItem));
        //    return;
        //}

        InventoryManager.Instance.Swap(dropItem.inventoryItem, transform.GetSiblingIndex(), dropItem.transform.GetSiblingIndex());
        InventoryManager.Instance.UIType(dropItem.inventoryItem).UpdateUI(InventoryManager.Instance.GetUIState(dropItem.inventoryItem));

        Debug.Log(transform.GetSiblingIndex());
        Debug.Log(InventoryManager.Instance.equipments[transform.GetSiblingIndex()]);
        Debug.Log(dropItem.transform.GetSiblingIndex());
        Debug.Log(InventoryManager.Instance.equipments[dropItem.transform.GetSiblingIndex()]);
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

    public void Swap(InventorySlot slot1, InventorySlot slot2)
    {
        InventorySlot temp = slot1;
        slot1 = slot2;
        slot2 = temp;
    }
}
