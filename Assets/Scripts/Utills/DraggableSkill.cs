using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableSkill : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Image image;
    private SkillSlot slot;
    [SerializeField]
    private Image tempImage;

    private void Awake()
    {
        image = GetComponent<Image>();
        slot = GetComponent<SkillSlot>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (PlayerStatManager.Instance.level > slot.skill.data.canLevel)
        {
            tempImage.sprite = image.sprite;
            tempImage.gameObject.SetActive(true);
            tempImage.raycastTarget = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        tempImage.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        tempImage.gameObject.SetActive(false);
        tempImage.raycastTarget = true;
    }
}
