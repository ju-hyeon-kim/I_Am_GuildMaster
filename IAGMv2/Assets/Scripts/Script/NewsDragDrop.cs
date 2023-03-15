using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NewsDragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    Vector2 dragOffset = Vector2.zero;
    // Start is called before the first frame update
    public void OnBeginDrag(PointerEventData eventData)
    {
        Image icon = transform.GetComponent<Image>();
        icon.raycastTarget = false;
        dragOffset = (Vector2)transform.position - eventData.position;
        
    }

    public void OnDrag(PointerEventData evenetData)
    {
        transform.position = evenetData.position + dragOffset;
    }
    
}
