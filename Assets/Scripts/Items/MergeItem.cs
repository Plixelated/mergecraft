using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class MergeItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector2 mousePOS;
    //private Transform currentParent;
    [SerializeField] private GameObject mergeObject;
    public Vector2 startPOS;
    public List<RaycastResult> hitObjects = new List<RaycastResult>();
    public int index;
    private Transform parent;

    public static Action<int,int> _merge;

    public static Action _refreshInvetory;

    private void OnEnable()
    {
        InputMonitor._mousePosition += GetMousePosition;
        PlayerInventory._invalidMerge += ReturnPosition;
        PlayerInventory._validMerge += Merge;
    }

    private void OnDisable()
    {
        InputMonitor._mousePosition -= GetMousePosition;
        PlayerInventory._invalidMerge -= ReturnPosition;
        PlayerInventory._validMerge -= Merge;
    }

    private void Start()
    {
        parent = this.gameObject.transform.parent;
        index = parent.GetComponent<ItemDisplay>().itemIndex;

    }

    private void GetMousePosition(Vector2 mousePosition)
    {
        mousePOS = mousePosition;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        startPOS = this.transform.position;
        if (index != parent.GetComponent<ItemDisplay>().itemIndex)
            index = index = parent.GetComponent<ItemDisplay>().itemIndex;
        //currentParent = transform.parent;
        //transform.SetParent(currentParent);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = mousePOS;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        mergeObject = GetMergeObject();
        
        if (mergeObject != null)
        {
            int targetIndex = mergeObject.transform.parent.GetComponent<ItemDisplay>().itemIndex;

            if (_merge != null)
                _merge(index,targetIndex);

        }
        else
            this.transform.position = startPOS;
    }

    private GameObject GetMergeObject()
    {
        var pointer = new PointerEventData(EventSystem.current);
        pointer.position = mousePOS;
        EventSystem.current.RaycastAll(pointer, hitObjects);

        if (hitObjects.Count > 0)
        {
            foreach (var hitObject in hitObjects)
            {
                if (hitObject.gameObject.tag == "inventory_item")
                {
                    if (hitObject.gameObject != this.gameObject)
                        return hitObject.gameObject;
                }
            }
        }

        return null;
    }

    private void ReturnPosition(int i)
    {
        if(i == index)
        {
            this.transform.position = startPOS;

            if(_refreshInvetory != null)
            {
                _refreshInvetory();
            }
        }
    }

    private void Merge(int i)
    {
        if (i == index)
        {
            parent.GetComponent<ItemDisplay>().itemTitle.text = "";
            parent.GetComponent<ItemDisplay>().itemIndex = -1;
        }
    }

}
