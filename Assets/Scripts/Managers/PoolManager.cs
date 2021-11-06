using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
    class Pool
    {
        public GameObject Original { get; private set; }
        public Transform Root { get; set; }

        Stack<Poolable> _poolStack = new Stack<Poolable>();

        public void init(GameObject original, int count = 5)
        {
            Original = original;
            Root = new GameObject().transform; // ������ Root�� Transform���� �����س���
            Root.name = $"{original.name}_Root";

            for(int i =0; i < 5; i++)
            {
                Push(Create());
            }
        }

        Poolable Create()
        {
            GameObject go = Object.Instantiate<GameObject>(Original);
            go.name = Original.name;
            return go.GetOrAddComponent<Poolable>();

        }

        public void Push(Poolable poolable)
        {
            if(poolable == null)
            {
                return;
                // ���ٸ� �ٷ� ������.
            }
            poolable.transform.parent = Root;
            // ���� ������ �κ�
            poolable.gameObject.SetActive(false);
            poolable.isUsing = false;

            // �̷��Ա����ؼ� ������ �Ϸ�Ǿ����� stack�� �־��ָ�ȴ�.
            _poolStack.Push(poolable);
            
        }
    }

    Dictionary<string, Pool> _pool = new Dictionary<string, Pool>();

    Transform _root;


    public void init()
    {
        if(_root == null)
        {
            _root = new GameObject { name = "@Pool_Root" }.transform;
            Object.DontDestroyOnLoad(_root);
        }
    }

    public void Push(Poolable poolable)
    {

    }

    public Poolable Pop(GameObject original, Transform parent = null)
    {

        return null;
    }

    public GameObject GetOriginal(string name)
    {
        return null;
    }
}
