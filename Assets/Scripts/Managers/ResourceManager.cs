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
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");
        if (prefab == null)
        {
            Debug.Log($"Fail to load prefab : {path}");
            return null;
        }
        
        GameObject go = UnityEngine.Object.Instantiate(prefab, parent);
        int index = go.name.IndexOf("(Clone)");
        if(index > 0)
            go.name = go.name.Substring(0, index); // 0번에서 index 번까지 짜른다 

        return go;
    }

    public void Destroy(GameObject go)
    {
        if (go == null)
            return;
        UnityEngine.Object.Destroy(go);
    }
}
