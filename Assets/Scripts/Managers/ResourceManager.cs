using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class ResourceManager
{
    public T Load<T>(string path) where T : UnityEngine.Object
    {
        if(typeof(T) == typeof(GameObject))
        {
            string name = path;
            int index = name.LastIndexOf('/');

            if (index >= 0)
                name = name.Substring(index + 1);


            GameObject go = Managers.Pool.GetOriginal(name);
            if (go != null)
                return go as T;

        }
        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject original = Load<GameObject>($"Prefabs/{path}");
        if (original == null)
        {
            Debug.Log($"Fail to load prefab : {path}");
            return null;
        }
        
        // 2. 혹시 풀링된 애가 있을까? 
        if (original.GetComponent<Poolable>() != null) // 없다면 무시하고 원래대로 돌아가면 될것이다.
            return Managers.Pool.Pop(original, parent).gameObject;

        GameObject go = UnityEngine.Object.Instantiate(original, parent);

        //int index = go.name.IndexOf("(Clone)");
        //if (index > 0)
        //    go.name = go.name.Substring(0, index);
        // 위에 3줄이랑 go.name = original.name;이랑 같은것이다.

        //GameObject go = Object.Instantiate(original, parent); 이렇게하는거랑 UnityEngine.Object.Instantiate(original, parent); 이거랑 같다
        go.name = original.name;
        return go;
    }

    public void Destroy(GameObject go)
    {
        if (go == null)
            return;

        // 3. 필요없다고 바로 없애는것이 아니라(풀링이 필요한 아이라면) -> 풀링매니저한테 위탁을 해준다.
        Poolable poolable = go.GetComponent<Poolable>();
        if(poolable != null)
        {
            Managers.Pool.Push(poolable);
            return;
        }

        UnityEngine.Object.Destroy(go);
        //Object.Destrot(go);
    }
}
