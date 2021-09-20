using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    int _order = 0; // 현재까지 최근에 사용한 ORDER를 저장.

    // 이제 팝업 목록을 들고 있어야하는데 이것을 어떤 자료구조로 들고있을지 생각해보면 스택 자료구조로 들고있는것이 좋다.
    Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();
    // 근데 <GameObject>를 들고있는것이 진짜 해답인지 생각해보면 -> 아니다.

    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/Popup/{name}");
        // 프리팹에 있는거 가져온

        T popup = Util.GetOrComponent<T>(go);
        _popupStack.Push(popup);
        Debug.Log("!!!!");

        //_order++; 여기서 일단 이렇게 안하는 이유는 ShowPopupUI 인터페이스로 띄운게 아니라,  누군가 드래그 드롭으로 씬에다가 시작할 때부터 만들어 놓는 경우,UI_BUTTON 생성해놓으면 숫자 안 올라가기 때문이다.(에러남)
        return popup;
    }

    public void ClosePopupUI(UI_Popup popup)
    {
        if (_popupStack.Count == 0)
            return;

        if(_popupStack.Peek() != popup)
        {   // PeeK는 꺼내는것이 아니라 엿보기만 하는것
            Debug.Log("Close Popup failed!");
            return;
        }

        ClosePopupUI();
    }

    public void ClosePopupUI()
    {
        if (_popupStack.Count == 0)
            return;
        // stack을 만질때는 항상 count를 생각해주자.(안에 아무것도 없을 수도 있으니까)
        UI_Popup popup = _popupStack.Pop();
        Managers.Resource.Destroy(popup.gameObject);
        popup = null;

        _order--;
    }

    public void CloseAllPopupUI()
    {
        //while (_popupStack.Count > 0)
        //    ClosePopupUI();
    }
}
