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
        texts[1].text = "설명 : " + itemData.description;
        texts[4].text = itemData.cost.ToString() + "원";
        texts[2].text = "등급 : " + itemData.rank.ToString();
        if (itemData.equipKind == EquipmentKind.Weapon)
        {
            WeaponData weapon = itemData as WeaponData;
            texts[3].text = "공격력 : " + weapon.damage.ToString() + "\n원소력 : " + weapon.elementalPower.ToString()
                + "\n크리티컬 확률 :" + weapon.criticalPercent.ToString() + "%\n 크리티컬 데미지 : " +
                weapon.criticalDamage.ToString() + "%";
        }
        else if (itemData.equipKind == EquipmentKind.Accessory)
        {
            AccessoryData accessory = itemData as AccessoryData;
            texts[3].text = "크리티컬 확률 : " + accessory.criticalPercent.ToString() + "%\n크리티컬 데미지 : " +
                accessory.criticalDamage.ToString() + "%\n회피율 : " + accessory.dodgeRate.ToString() + "%";
        }
        else
        {
            Clothes clothes = itemData as Clothes;
            texts[3].text = "체력 : " + clothes.hp.ToString() + "\n방어력 : " + clothes.defence.ToString();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        equipUI.transform.SetParent(originalPos);
        equipUI.gameObject.SetActive(false);
    }
}
