using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopController : MonoBehaviour, IDropHandler
{
    [SerializeField]
    private Shop[] shops;

    public void OnDrop(PointerEventData eventData)
    {
       DraggableItem dragItem = eventData.pointerDrag.GetComponent<DraggableItem>();
        if (dragItem == null)
            return;

        InventorySlot dropItem = dragItem.parent.GetComponent<InventorySlot>();
        if (dropItem == null)
            return;

        GameManager.Instance.money += (dropItem.inventoryItem.data.cost / 2);
        InventoryManager.Instance.RemoveItem(dropItem.inventoryItem);
    }

    public void UpdateShop()
    {
        shops = GetComponentsInChildren<Shop>();
        for (int i = 0; i < shops.Length; i++)
        {
            shops[i].Release();
        }
    }
}
