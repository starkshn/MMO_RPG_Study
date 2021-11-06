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
            Root = new GameObject().transform; // 위에서 Root를 Transform으로 설정해놔서
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
                // 없다면 바로 끝낸다.
            }
            poolable.transform.parent = Root;
            // 영상 꺼놓는 부분
            poolable.gameObject.SetActive(false);
            poolable.isUsing = false;

            // 이렇게까지해서 설정이 완료되었으니 stack에 넣어주면된다.
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
