using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // 쿼터뷰를 사용할것인데 ( 쿼터뷰가 뭔지 모르겟다 )

    [SerializeField]
    Define.CameraMode _mode = Define.CameraMode.QuarterView;
    

    [SerializeField]
    Vector3 _delta = new Vector3(0.0f, 5.0f, -5.0f); // _delta 라는 것은 player 기준으로 얼마만큼 떨어져있는지를 나타내는것임

    [SerializeField]
    GameObject _player = null;


    // [SerializeField] 하면 public 붙인거랑 똑같이 유니티에서도 뜬다.

    void Start()
    {

    }


    void LateUpdate() // LateUpdate로 한 이유 생각!
    {
        if(_mode == Define.CameraMode.QuarterView)
        {
            RaycastHit hit;

            if(_player == null)
            {

                return;
            }

            if(Physics.Raycast(_player.transform.position, _delta, out hit, _delta.magnitude, LayerMask.GetMask("Wall")))
            {
                // if true라는 것은 벽을 만났다는 말이니까
                float dist = (hit.point - _player.transform.position).magnitude * 0.8f;
                // 이렇게하면 방향 벡터의 크기가 나온다. 0.8f 곱한것은 벽보다 살짝 앞으로 위치시키기 위해서이다.
                transform.position = _player.transform.position + _delta.normalized * dist;

            }
            else
            {
                //Player position, camera 위치, hit을 out으로 넣어준다

                transform.position = _player.transform.position + _delta;
                transform.LookAt(_player.transform);

            }
        }
    }

    public void SetQuarterView(Vector3 delta)
    {
        _mode = Define.CameraMode.QuarterView;
        _delta = delta;
    }
}
