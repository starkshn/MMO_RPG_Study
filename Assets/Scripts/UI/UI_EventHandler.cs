using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EventHandler : MonoBehaviour, IBeginDragHandler, IDragHandler // 이러한 인터페이스를 추가한
{
    // 일단 Component로 사용했으니 모노는 살려두자.

    public Action<PointerEventData> OnBeginDragHandler = null;
    public Action<PointerEventData> OnDragHandler = null;

    // 그런데 Event System에서 던져주는 event를 받을려면 특정 인터페이스에 맞는 함수들을 정의하고 있어야되는데

    public void OnBeginDrag(PointerEventData eventData)
    {
        
        if (OnBeginDragHandler != null)
            OnBeginDragHandler.Invoke(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        //transform.position = eventData.position; 이부분에서 드래그하면 드래그됬었다.
        if (OnDragHandler != null)
            OnDragHandler.Invoke(eventData);
    }
}
