using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{

    public BaseScene CurrenScene { get { return GameObject.FindObjectOfType<BaseScene>(); } } // property로 만든것.
    // 이런식으로 BaseScene Component를 들고 있는 애를 찾아주세요이다.

    public void  LoadScene(Define.Scene type)
    {
        Managers.Clear();
        // CurrenScene.Clear(); // 현재 Scene을 찾아가지고 Clear로 날려준다음에 다음 씬으로 이동(SceneManager.LoadScene() 이부분.) => SoundManager#3에서 clear()구현하면서 밑으로 옮김.
        SceneManager.LoadScene(GetSceneName(type));
        // LoadScene에 string을 넣어줬는데 Define은 Define값이지 string이 아니다. 그래서 Define.Scene을 string으로 받아주는 함수 만들자
    }

    public string GetSceneName(Define.Scene type)
    {
        // 지금 이 함수는 C++에서는 구현을 할 수가 없다! Reflection을 지원하지 않아서(Csharp는 Refletion지원한다)
        string name = System.Enum.GetName(typeof(Define.Scene), type); // 이렇게하면 Define.Scene enum의 value추출 가능
        return name;
    }

    public void Clear()
    {
        CurrenScene.Clear();
    }
}
