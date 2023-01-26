using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField]
    private Image icon;
    [SerializeField]
    private TextMeshProUGUI cost;
    [SerializeField]
    private ShopItemInfo itemInfo;

    private ItemData itemData;
    private void Awake()
    {
        itemInfo = GetComponentInChildren<ShopItemInfo>();
    }

    public void Set(ItemData itemData)
    {
        this.itemData = itemData;
        icon.sprite = itemData.itemIcon;
        cost.text = itemData.cost.ToString() + "¿ø";
        itemInfo.itemData = itemData;
    }

    public void Release()
    {
        PoolManager.Instance.Release(this.gameObject);
    }

    public void BuyItem()
    {
        if (itemData == null)
            return;

        if (itemData.cost > GameManager.Instance.money)
            return;

        InventoryItem buyItem = new InventoryItem();
        buyItem.data = itemData;
        InventoryManager.Instance.AddItem(buyItem);
        GameManager.Instance.money -= itemData.cost;
    }
}
