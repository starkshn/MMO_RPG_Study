using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{

    static Managers s_instance; // 유일성 보장된다

    // 외부에서 얘를 사용하고싶을때는
    static Managers Instance { get { init(); return s_instance; } } // 유일한 매니저를 갖고온다 

    #region Contents
    GameManagerEx _game = new GameManagerEx();
    public static GameManagerEx Game { get { return Instance._game;  } }
    #endregion

    #region Core
    DataManager _data = new DataManager();
    InputManager _input = new InputManager();
    PoolManager _pool = new PoolManager();
    ResourceManager _resource = new ResourceManager();
    SoundManager _sound = new SoundManager();
    SceneManagerEx _scene = new SceneManagerEx();
    UIManager _ui = new UIManager();

    public static DataManager Data { get { return Instance._data;  } }
    public static InputManager Input { get { return Instance._input;  } }
    public static PoolManager Pool { get { return Instance._pool; } }
    public static ResourceManager Resource {  get { return Instance._resource;  } }
    public static SceneManagerEx Scene { get { return Instance._scene; } } // SceneManager는 UI위에 위치하니까 여기다가 넣어주자
    public static SoundManager Sound { get { return Instance._sound; } }
    public static UIManager UI { get { return Instance._ui; } }
    #endregion


    void Start()
    {
        init();
    }

    // Update is called once per frame
    void Update()
    {
        _input.OnUpdate();
    }

    static void init() 
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

            // 여기서 Instance 호출하면 안된다! 주의 해야함 왜냐하면 static Managers Instance { get { Init(); return s_instance; } }
            // 에서 return으로 Init()을 호출하고있기때문에 무한루프에 빠질 수 있다.


            s_instance._data.init();
            s_instance._sound.init();

            s_instance._pool.init(); // pool에 init하는 부분이 있었으니까 챙겨주자

        }
    }

    public static void Clear()
    {
        Input.Clear();
        Sound.Clear();
        Scene.Clear();
        UI.Clear();

        Pool.Clear();
    }
}
