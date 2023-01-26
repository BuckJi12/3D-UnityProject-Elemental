using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopItemInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Image equipUI;

    [SerializeField]
    private Image equipIcon;

    private TextMeshProUGUI[] texts;

    public ItemData itemData;

    public Transform originalPos;

    private void Start()
    {
        texts = equipUI.GetComponentsInChildren<TextMeshProUGUI>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (itemData == null)
            return;

        equipUI.transform.SetParent(transform.root);
        equipUI.transform.position = Input.mousePosition + new Vector3(300, -300);
        equipUI.gameObject.SetActive(true);
        texts[0].text = itemData.name;
        equipIcon.sprite = itemData.itemIcon;
        texts[1].text = "���� : " + itemData.description;
        texts[4].text = itemData.cost.ToString() + "��";
        texts[2].text = "��� : " + itemData.rank.ToString();
        if (itemData.equipKind == EquipmentKind.Weapon)
        {
            WeaponData weapon = itemData as WeaponData;
            texts[3].text = "���ݷ� : " + weapon.damage.ToString() + "\n���ҷ� : " + weapon.elementalPower.ToString()
                + "\nũ��Ƽ�� Ȯ�� :" + weapon.criticalPercent.ToString() + "%\n ũ��Ƽ�� ������ : " +
                weapon.criticalDamage.ToString() + "%";
        }
        else if (itemData.equipKind == EquipmentKind.Accessory)
        {
            AccessoryData accessory = itemData as AccessoryData;
            texts[3].text = "ũ��Ƽ�� Ȯ�� : " + accessory.criticalPercent.ToString() + "%\nũ��Ƽ�� ������ : " +
                accessory.criticalDamage.ToString() + "%\nȸ���� : " + accessory.dodgeRate.ToString() + "%";
        }
        else
        {
            Clothes clothes = itemData as Clothes;
            texts[3].text = "ü�� : " + clothes.hp.ToString() + "\n���� : " + clothes.defence.ToString();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        equipUI.transform.SetParent(originalPos);
        equipUI.gameObject.SetActive(false);
    }
}
