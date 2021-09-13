using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{

    static Managers s_instance; // 유일성 보장된다

    // 외부에서 얘를 사용하고싶을때는
    static Managers Instance { get { Init(); return s_instance; } } // 유일한 매니저를 갖고온다 

    InputManager _input = new InputManager();
    public static InputManager Input { get { return Instance._input;  } }
    // InputManager 를 반환하는데 Input이라는 함수 이름으로.

    // ResourceManager
    ResourceManager _resource = new ResourceManager();
    public static ResourceManager Resource {  get { return Instance._resource;  } }
    
    
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        _input.OnUpdate();
    }

    static void Init() 
    {
        if (s_instance == null) 
        {
            // 초기화
            GameObject go = GameObject.Find("@Managers");
            if(go == null) 
            {
                go = new GameObject {name = "@Managers"};
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();

        }
    }
}
