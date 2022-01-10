using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UI_Base : MonoBehaviour
{

    Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

    public abstract void init();

    private void Start()
    {
        init();
    }

    protected void Bind<T>(Type type) where T : UnityEngine.Object 
    {
        string[] names = Enum.GetNames(type);

        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        
        _objects.Add(typeof(T), objects);

        for (int i = 0; i < names.Length; i++)
        {
            if (typeof(T) == typeof(GameObject)) // GameObject 전용버젼을 만들것이다.
                objects[i] = Util.FindChild(gameObject, names[i], true);

            else
                objects[i] = Util.FindChild<T>(gameObject, names[i], true);
          
            if (objects[i] == null)
                Debug.Log($"Failed to Bind!{names[i]}");

        }
    }

    protected T Get<T>(int idx) where T : UnityEngine.Object 
    {
        UnityEngine.Object[] objects = null;
        if (_objects.TryGetValue(typeof(T), out objects) == false) 
            return null;

        return objects[idx] as T; 
    }

    // GameObject를 가장 많이 만드는데 이부분 proteted Text처럼 만들어주자
    protected GameObject GetObject(int idx) { return Get<GameObject>(idx); }
    protected Text GetText(int idx) { return Get<Text>(idx); }
    protected Button GetButton(int idx) { return Get<Button>(idx); }
    protected Image GetImage(int idx) { return Get<Image>(idx); }

    public static void BindEvent(GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
    {
        // 먼저 게임오브젝트를 받고, 어떠 콜백으로 받을 함수(연동할 함수), 어떤 아이한테 연결을 해줄?(OnDragHandler도 있지만 다른 애들도 있을 수 있기 때문에

        //UI_EventHandler evt = go.GetComponent<UI_EventHandler>();// 있다고 가정을하면 안되기 때문에(자주 쓰이니까 UTILS에다가 옮기자)
        //if (evt == null)
        //    evt = go.AddComponent<UI_EventHandler>();

        UI_EventHandler evt = Util.GetOrAddComponent<UI_EventHandler>(go); // 이렇게 컴포넌트, 추가했으니 type에 따라서 연동을 키져주자
        switch(type)
        {
            case Define.UIEvent.Click:
                evt.OnClickHandler -= action;
                evt.OnClickHandler += action;
                break;

            case Define.UIEvent.Drag:
                evt.OnDragHandler -= action;
                evt.OnDragHandler += action;
                break;

        }

        // gameobject를 받아와서 UI_EventHandler라는 컴포넌트를 추출을 한다음 구독을 추가해주는 방식으로 이루어졌었다.
        //evt.OnDragHandler += ((PointerEventData data) => { evt.gameObject.transform.position = data.position; });
    }
}
