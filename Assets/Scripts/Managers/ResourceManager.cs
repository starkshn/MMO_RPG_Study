using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class ResourceManager
{
    public T Load<T>(string path) where T : UnityEngine.Object
    {
        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {

        // 1. original 들고있다면 바로 사용
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");
        if (prefab == null)
        {
            Debug.Log($"Fail to load prefab : {path}");
            return null;
        }
        

        // 2. 혹시 풀링된 애가 있을까? 
        GameObject go = UnityEngine.Object.Instantiate(prefab, parent);
        //int index = go.name.IndexOf("(Clone)");
        //if(index > 0)
        //    go.name = go.name.Substring(0, index); // 0번에서 index 번까지 짜른다 
        // 사실 이렇게 힘들게 할필요는 없고
        go.name = prefab.name;

        return go;
    }

    public void Destroy(GameObject go)
    {
        if (go == null)
            return;
        UnityEngine.Object.Destroy(go);
    }
}
