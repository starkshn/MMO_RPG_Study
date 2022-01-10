using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // 이녀석을 import 해주어야 OnUpdate()안에서 Eventsystem사용가능

public class InputManager
{
    public Action KeyAction = null; // 얘는 일종의 delegate 이다.
    public Action<Define.MouseEvent> MouseAction = null;

    public Action<Define.KeyBoardEvent> KeyBoardAction = null;

    bool _presssed = false;

    public bool _pressed_space = false; // jump 키 

    float _pressedTime = 0;

    public void OnUpdate()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        // InputManager 가 키보드 입력 대표로 받고 입력이 있으면 걔를 event로 쫙 전파를 해준다 => 이것이 리스너 패턴.
        // update 함수에서 InputManager 가 대표 입력을 체크를 한다음 만약에 실제로 입력이 있었으면 event를 쫙 전파를 해준는 형식으로 구현 할것이다 => 이것이 리스너 패턴이다.
        if(Input.anyKey && KeyAction != null)
        {
            KeyAction.Invoke();
        }
        if(MouseAction != null)
        {
            if(Input.GetMouseButton(0))
            {
                // 왼쪽 클릭은 0번이다.
                //GetMouseButton 누르고만 있어도 무조건 뜨는거고 GetMouseButtonDown 은 처음에 누를때만 이벤트가 들어온다.
                if(!_presssed)
                {
                    MouseAction.Invoke(Define.MouseEvent.PointerDown);
                    _pressedTime = Time.time;
                }

                MouseAction.Invoke(Define.MouseEvent.Press);
                _presssed = true;
            }
            else
            {
                if (_presssed)
                {
                    if(Time.time < _pressedTime + 0.2f)
                        MouseAction.Invoke(Define.MouseEvent.Click);

                    MouseAction.Invoke(Define.MouseEvent.PointerUp);
                }
                    
                _presssed = false;
                _pressedTime = 0;
            }
        }

        //키보드로 Spcaebar 입력 받았을 때
        //if (KeyBoardAction != null)
        //{
        //    if (Input.GetKeyDown(KeyCode.Space))
        //    {
        //        Debug.Log("스페이스바 눌림!");
        //        KeyBoardAction.Invoke(Define.KeyBoardEvent.P_Space);
        //        _pressed_space = true;
        //    }
        //    else
        //    {
        //        if (_pressed_space)
        //        {
        //            Debug.Log("현재는 트루 좀있다 false");
        //            _pressed_space = false;

        //        }
        //    }
        //}
    }

    public void Clear()
    {
        KeyAction = null;
        MouseAction = null;

    }
}
