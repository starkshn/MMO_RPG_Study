using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{

    static Managers s_instance; // 유일성 보장된다

    // 외부에서 얘를 사용하고싶을때는
    static Managers Instance { get { Init(); return s_instance; } } // 유일한 매니저를 갖고온다 

    InputManager _input = new InputManager();
    ResourceManager _resource = new ResourceManager();
    SoundManager _sound = new SoundManager();
    SceneManagerEx _scene = new SceneManagerEx();
    UIManager ui = new UIManager();

    public static InputManager Input { get { return Instance._input;  } }
   
    public static ResourceManager Resource {  get { return Instance._resource;  } }
    public static SceneManagerEx Scene { get { return Instance._scene; } } // SceneManager는 UI위에 위치하니까 여기다가 넣어주자
    public static SoundManager Sound { get { return Instance._sound; } }
    public static UIManager UI { get { return Instance.ui; } }

    
    
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
