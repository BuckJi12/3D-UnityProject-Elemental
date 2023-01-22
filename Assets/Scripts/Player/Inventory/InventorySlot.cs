using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler, IPointerClickHandler, IPointerEnterHandler,
    IPointerExitHandler
{
    [SerializeField]
    private Image itemIcon;

    [Header("EquipUI")]
    [SerializeField]
    private Image equipUI;
    [SerializeField]
    private Image equipIcon;
    [SerializeField]
    private TextMeshProUGUI Title;
    [SerializeField]
    private TextMeshProUGUI description;
    [SerializeField]
    private TextMeshProUGUI rank;
    [SerializeField]
    private TextMeshProUGUI stat;
    [SerializeField]
    private TextMeshProUGUI cost;

    [Header("MaterialUI")]
    [SerializeField]
    private Image materialUI;
    [SerializeField]
    private TextMeshProUGUI materialTitle;
    [SerializeField]
    private Image materialIcon;
    [SerializeField]
    private TextMeshProUGUI materialDescription;

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
        if (dragItem == null)
            return;

        InventorySlot dropItem = dragItem.parent.GetComponent<InventorySlot>();

        if (dropItem == null)
            return;

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
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (inventoryItem != null)
        {
            switch (inventoryItem.data.kind)
            {
                case ItemKind.Equipment:
                    equipUI.transform.position = Input.mousePosition + new Vector3(300, -300);
                    equipUI.gameObject.SetActive(true);
                    Title.text = inventoryItem.data.name;
                    equipIcon.sprite = inventoryItem.data.itemIcon;
                    description.text = "설명 : " + inventoryItem.data.description;
                    cost.text = inventoryItem.data.cost.ToString() + "원";
                    rank.text = "등급 : " + inventoryItem.data.rank.ToString();
                    if (inventoryItem.data.equipKind == EquipmentKind.Weapon)
                    {
                        WeaponData weapon = inventoryItem.data as WeaponData;
                        stat.text = "공격력 : " + weapon.damage.ToString() + "\n원소력 : " + weapon.elementalPower.ToString()
                            + "\n크리티컬 확률 :" + weapon.criticalPercent.ToString() + "%\n 크리티컬 데미지 : " +
                            weapon.criticalDamage.ToString() + "%";
                    }
                    else if (inventoryItem.data.equipKind == EquipmentKind.Accessory)
                    {
                        AccessoryData accessory = inventoryItem.data as AccessoryData;
                        stat.text = "크리티컬 확률 : " + accessory.criticalPercent.ToString() + "%\n크리티컬 데미지 : " +
                            accessory.criticalDamage.ToString() + "%\n회피율 : " + accessory.dodgeRate.ToString() + "%";
                    }
                    else
                    {
                        Clothes clothes = inventoryItem.data as Clothes;
                        stat.text = "체력 : " + clothes.hp.ToString() + "\n방어력 : " + clothes.defence.ToString();
                    }
                    break;
                case ItemKind.UsableItem:
                    materialUI.transform.position = Input.mousePosition + new Vector3(300, -300);
                    materialUI.gameObject.SetActive(true);
                    materialIcon.sprite = inventoryItem.data.itemIcon;
                    materialTitle.text = inventoryItem.data.name;
                    materialDescription.text = inventoryItem.data.description.ToString();
                    break;
                case ItemKind.Materialitem:
                    materialUI.transform.position = Input.mousePosition + new Vector3(300, -300);
                    materialUI.gameObject.SetActive(true);
                    materialIcon.sprite = inventoryItem.data.itemIcon;
                    materialTitle.text = inventoryItem.data.name;
                    materialDescription.text = inventoryItem.data.description.ToString();
                    break;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        equipUI.gameObject.SetActive(false);
        materialUI.gameObject.SetActive(false);
    }
}
