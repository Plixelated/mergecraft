using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector2 mousePOS;
    //private Transform currentParent;
    private GameObject mergeObject;
    private Vector2 startPOS;
    public List<RaycastResult> hitObjects = new List<RaycastResult>();

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
        if (mergeObject != null)
        {
            this.gameObject.GetComponent<PotionDisplay>().nameText.text = "";
            this.gameObject.SetActive(false);
            Merge();
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
               if(hitObject.gameObject.tag == "inventory_item")
                {
                    if(hitObject.gameObject != this.gameObject)
                        return hitObject.gameObject;
                }
            }
        }
        
        return null;
    }

    private void Merge()
    {
        var currentPotion = this.gameObject.GetComponent<PotionDisplay>().potion;
        var mergePotion = mergeObject.gameObject.GetComponent<PotionDisplay>().potion;
        var newPotion = this.gameObject.GetComponent<PotionDisplay>().upgradePotion;

        Debug.Log($"Merging {currentPotion} with {mergePotion}");
        if (currentPotion.name == "Base Potion" && mergePotion.name == "Base Potion") 
        {
            mergeObject.GetComponent<PotionDisplay>().DisplayPotion(newPotion);
        }
    }
}
