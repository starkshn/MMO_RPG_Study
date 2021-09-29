using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseScene : MonoBehaviour
{
    // 모든 씬에대한 최상위 부모이다.
    // Define.Scene _sceneType = Define.Scene.Unknown 밑에처럼 수정
    public Define.Scene SceneType { get; protected set; } = Define.Scene.Unknown;

    void Start()
    {
        
    }

    
    protected virtual void init()
    {

    }

    public abstract void Clear(); // 당장 여기서 정의 할것이 아니니까 abstarct로 


}
