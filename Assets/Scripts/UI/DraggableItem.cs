using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Start Dragon");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Am Dragon");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Stop Dragon");
    }
}
