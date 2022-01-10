using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerEx
{
    // int <-> GameObject


    GameObject _player;
    HashSet<GameObject> _monsters = new HashSet<GameObject>();
    //Dictionary<int, GameObject> _player = new Dictionary<int, GameObject>(); 나중에 플레이어가 많아지면 사용하자 , 지금은 본인밖에 없으니까
    
    public GameObject spawn(Define.WorldObject type, string path, Transform parent = null)
    {

        GameObject go = Managers.Resource.Instantiate(path, parent);

        switch(type)
        {
            case Define.WorldObject.Monster:
                _monsters.Add(go);
                break;
            case Define.WorldObject.Player:
                _player = go;
                break;
        }

        return go; 
    }

    public Define.WorldObject GetWorldObjectType(GameObject go)
    {
        BaseController bc = go.GetComponent<BaseController>();

        if (bc == null)
            return Define.WorldObject.Unknown;

        // bc is PlayerController; 이렇게 체크를 할 수는 있기는 하다.
        return bc.WorldObjectType;
    }

    public void Despawn(GameObject go)
    {
        Define.WorldObject type = GetWorldObjectType(go);

        switch(type)
        {
            case Define.WorldObject.Player:
                {
                    if (_player == go)
                        _player = null;
                }
                break;
            case Define.WorldObject.Monster:
                {
                    if (_monsters.Contains(go))
                        _monsters.Remove(go);
                }
                
                break;
        }

        Managers.Resource.Destroy(go);
    }
}

