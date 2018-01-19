using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropHandlerPicks : MonoBehaviour, IDropHandler
{       

    
    private int maxBuilding = 3;

    public void OnDrop(PointerEventData eventData)
    {   
        if(transform.childCount < 3)
        {
            DragHandler.itemDragged.transform.SetParent(gameObject.transform);
        }
        
    }
}
