using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler{

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

            if(eventData.pointerDrag.GetComponent<DragDrop>().canAttack == true)
            {
                Attack.canAttack = true;
            }
        }
    }
}
