using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public BuildingAsset buildingAsset;
    public static GameObject itemDragged;
    private Vector3 startPosition;
    private Transform startParent;

    public void OnBeginDrag(PointerEventData eventData)
    {   
        itemDragged = gameObject;
        startPosition = gameObject.transform.position;
        startParent = gameObject.transform.parent;
        transform.SetParent(startParent.parent);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        itemDragged.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        itemDragged = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        if (transform.parent == startParent.parent)
        {
            transform.SetParent(startParent);
            transform.position = startPosition;
        }
    }

  
}
