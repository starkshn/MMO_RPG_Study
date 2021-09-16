using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public static class Extension
{
    // static 오로지 하나만 존재

    // 그리고 추가해줄 Extension을 넣어줘야함
    // 지금 Extension 해주고 싶은 함수 부분이UI_Button에서 AddUIEvent(go,((PointerEventData data) => { go.gameObject.transform.position = data.position; }), Define.UIEvent.Drag);
    // 쉬발 이부분이다.

    public static void AddUIEvent(this GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
    {
        UI_Base.AddUIEvent(go, action, type);

    }


}



