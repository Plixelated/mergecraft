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
    private Vector2 startPOS;
    public List<RaycastResult> hitObjects = new List<RaycastResult>();

    public static Action<int,int> _merge;

    private void OnEnable()
    {
        InputMonitor._mousePosition += GetMousePosition;
    }

    private void OnDisable()
    {
        InputMonitor._mousePosition -= GetMousePosition;
    }

    private void GetMousePosition(Vector2 mousePosition)
    {
        mousePOS = mousePosition;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        startPOS = this.transform.position;
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
        var mergeParent = mergeObject.transform.parent;
        if (mergeObject != null)
        {
            var parent = this.gameObject.transform.parent;
            parent.GetComponent<ItemDisplay>().itemTitle.text = "";

            if (_merge != null)
                _merge(parent.GetComponent<ItemDisplay>().itemIndex,mergeParent.GetComponent<ItemDisplay>().itemIndex);

            parent.gameObject.SetActive(false);

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

    /**private void Merge()
    {
        var currentPotion = this.gameObject.GetComponent<ItemDisplay>().potion;
        var mergePotion = mergeObject.gameObject.GetComponent<ItemDisplay>().potion;
        var newPotion = this.gameObject.GetComponent<ItemDisplay>().upgradePotion;

        Debug.Log($"Merging {currentPotion} with {mergePotion}");
        if (currentPotion.name == "Base Potion" && mergePotion.name == "Base Potion")
        {
            mergeObject.GetComponent<ItemDisplay>().DisplayPotion(newPotion);
        }
    }**/
}
