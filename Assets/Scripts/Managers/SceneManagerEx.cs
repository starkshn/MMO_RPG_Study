using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    public void  LoadScene(Define.Scene type)
    {
        //
        SceneManager.LoadScene(GetSceneName(type));
        // LoadScene에 string을 넣어줬는데 Define은 Define값이지 string이 아니다. 그래서 Define.Scene을 string으로 받아주는 함수 만들자
    }

    public string GetSceneName(Define.Scene type)
    {
        // 지금 이 함수는 C++에서는 구현을 할 수가 없다! Reflection을 지원하지 않아서(Csharp는 Refletion지원한다)
        string name = System.Enum.GetName(typeof(Define.Scene), type); // 이렇게하면 Define.Scene enum의 value추출 가능
        return name;
    }
}
