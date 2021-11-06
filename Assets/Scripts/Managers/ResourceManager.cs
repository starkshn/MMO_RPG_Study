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
        
        // 2. Ȥ�� Ǯ���� �ְ� ������? 
        if (original.GetComponent<Poolable>() != null) // ���ٸ� �����ϰ� ������� ���ư��� �ɰ��̴�.
            return Managers.Pool.Pop(original, parent).gameObject;

        GameObject go = UnityEngine.Object.Instantiate(original, parent);

        //int index = go.name.IndexOf("(Clone)");
        //if (index > 0)
        //    go.name = go.name.Substring(0, index);
        // ���� 3���̶� go.name = original.name;�̶� �������̴�.

        //GameObject go = Object.Instantiate(original, parent); �̷����ϴ°Ŷ� UnityEngine.Object.Instantiate(original, parent); �̰Ŷ� ����
        go.name = original.name;
        return go;
    }

    public void Destroy(GameObject go)
    {
        if (go == null)
            return;

        // 3. �ʿ���ٰ� �ٷ� ���ִ°��� �ƴ϶�(Ǯ���� �ʿ��� ���̶��) -> Ǯ���Ŵ������� ��Ź�� ���ش�.
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
