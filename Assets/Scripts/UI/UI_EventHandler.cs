using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EventHandler : MonoBehaviour, IPointerClickHandler, IDragHandler
{
    private Action<PointerEventData> onClickHandler = null;
    private Action<PointerEventData> onDragHandler = null;

    public Action<PointerEventData> OnClickHandler { get => onClickHandler; set => onClickHandler = value; }
    public Action<PointerEventData> OnDragHandler { get => onDragHandler; set => onDragHandler = value; }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (OnClickHandler != null)
            OnClickHandler.Invoke(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (OnDragHandler != null)
            OnDragHandler.Invoke(eventData);
    }


}
