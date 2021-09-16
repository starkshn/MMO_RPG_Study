using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
    // UI_Base 에서 Getcompnent하느느 부분가져옴 자주 쓰이기 때문에
    public static T GetOrComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();
        if (component == null)
            component = go.AddComponent<T>();

        return component;
    }



    // GameObject 전용 FIndChild 함수 만들어주자( 인터페이스는 비슷하니까 복붙하자 일단) 
    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false) // Generic 형식이 아니기 때문에 where T 삭제, 근데 모든 GameObject는 TRansform 컴포넌트를 가지고 있다. 그래서 컴포넌트 FindChild<T>를 호출해서 Trnasform으로 받는다.
    {
        Transform transform = FindChild<Transform>(go, name, recursive);
        if (transform == null)
            return null;

        return transform.gameObject;
    }

    // 여기다가 기능함 함수들만 넣어주면된다.
    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object  // 최상위 부모를 받고, 이름을 받는데 이름을 입력하지 않으면 이름은 비교하지않고 타입에만 해당하면 RETurn 해주자, boolean 받는다. recursive는 재귀적으로 찾을 것이냐 인데 자식을 찾았을때 그 자식만 찾을 것인지, 아니면 자식의 자식까지 쭉 스캔을 할것인지를 묻는 것이다. 
    {
        if (go == null)
            return null;

        // FindChild<T> T에 들어 오는 녀석은 Button이라거나 Text등등 찾고싶은 Component들을 넣어주게 될것이다.

        if(recursive == false)
        {
            // 여러가지 방법이 있기는한데 이 강사님이 찾은 방법은 다음과 같다.
            for(int i= 0; i < go.transform.childCount; i++) // childCount는 자식의 갯수
            {
               Transform transform = go.transform.GetChild(i); // 직속 자식을 찾는 것 파라미터는 몇번째 자식을 찾을 것인지 넘겨주는것
                // 이렇게하면은 내 자식들을 하나씩 스캔을 해주게 되겠다.

                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    // name 통과했다면 component를 가져와야한다. 가져올려먼 뭐? GetComponent사용
                    T component = transform.GetComponent<T>();
                    if (component != null)
                        return component;
                }


            }
        }
        else
        {
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                // component타입은 T타입이고 GameObject(go)가 가지고있는 T타입의 object를 스캔을 할것이고, 그녀석을 foreach로 돌리니까 (T component) 그것에 대한 각각의 순회를 하게된다.
                if (string.IsNullOrEmpty(name) || component.name == name) // name 이 비어있거나, 진짜 원하는 name이라면 name을 반환해준다.
                    return component;
            }
        }

        return null; // 못찾으면 그냥 ㅜULL로 return 
    }

}

