using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 clickPoint;
    private Vector3 posDifference;
    private Vector3 endPosition;
    public void OnBeginDrag(PointerEventData eventData)
    {
        clickPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
        posDifference = transform.position - clickPoint;
    }

    public void OnDrag(PointerEventData eventData)
    {
        endPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
        transform.position = endPosition + posDifference;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = endPosition + posDifference;
    }

}
