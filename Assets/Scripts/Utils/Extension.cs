using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public static class Extension
{
    // static 오로지 하나만 존재

    // 그리고 추가해줄 Extension을 넣어줘야함
    // 지금 Extension 해주고 싶은 함수 부분이UI_Button에서 BindEvent(go,((PointerEventData data) => { go.gameObject.transform.position = data.position; }), Define.UIEvent.Drag);
    // 쉬발 이부분이다.

    public static void BindEvent(this GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
    {
        UI_Base.BindEvent(go, action, type);
    }

    public static T GetOrAddComponent<T>(this GameObject go) where T : UnityEngine.Component
    {
       return Util.GetOrAddComponent<T>(go);
    }

    public static bool IsValid(this GameObject go)
    {
        return go != null && go.activeSelf;
    }
}



