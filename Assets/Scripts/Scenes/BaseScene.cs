using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseScene : MonoBehaviour
{
    // ��� �������� �ֻ��� �θ��̴�.
    // Define.Scene _sceneType = Define.Scene.Unknown �ؿ�ó�� ����
    public Define.Scene SceneType { get; protected set; } = Define.Scene.Unknown;

    void Start()
    {
        
    }

    
    protected virtual void init()
    {

    }

    public abstract void Clear(); // ���� ���⼭ ���� �Ұ��� �ƴϴϱ� abstarct�� 


}
