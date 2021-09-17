using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _speed = 10.0f;

    Vector3 _destPos;

    InputManager isSpace = new InputManager();
    bool Jump = false;
    

    void Start()
    {
        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;

        Managers.Input.KeyBoardAction -= OnKeyBoardPressed;
        Managers.Input.KeyBoardAction += OnKeyBoardPressed;

        // KeyBoardAction은 delegate 이기때문에 OnkeyBoardPressed로 구독을 해준다.

        // 여기에서 UI를 부르는것을 해보도록하자
        //Managers.Resource.Instantiate("UI/UI_Button");  // 여기에다가 경로를 입력 UI자동화 #3에서 잠시 주석처리함(귀찮아서)


        // TEMP
        UI_Button ui = Managers.UI.ShowPopupUI<UI_Button>("UI_Button");

        Managers.UI.ClosePopupUI(ui); // 호출하면 마지막으로 띄운 팝업 닫

    }

    // State 패턴은 상태를 정의하게된다.
    public enum PlayerState
    {
        Die,
        Moving,
        Idle,
        Jump,
        Sliding,

    }

    PlayerState _state = PlayerState.Idle;

    void UpdateDie()
    {
        // 아무것도 못함
    }

    void UpdateMoving()
    {
        // 이동하는 상태
        Vector3 dir = _destPos - transform.position;
        if (dir.magnitude < 0.00001f) // 도착했을때 정확하게 0이 안나온다 float끼리 뺼 때
        {
            //_moveToDest = false; 목적지에 도달한 상태이니까
            _state = PlayerState.Idle;

        }
        else
        {
            // else 니까 이동하는 상태
            float _moveDist = Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude);
            transform.position += dir.normalized * _moveDist;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);
        }

        

        // 애니매이션
        // wait_run_ratio값을 1로 일정시간 동안 옮겨주세요라는뜻, 뒤에 Time.deltaTime 부분은 자연스럽게 되도록 조절 해야함
        Animator anim = GetComponent<Animator>();
        // 현재 게임상태에 대한 정보를 넘겨준다.
        anim.SetFloat("speed", _speed);

        if (Input.GetKeyDown(KeyCode.A))
        {
            _state = PlayerState.Sliding;
            
        }

    }

    //void OnRunEvent(int a)
    //{
    //    Debug.Log($"왼발! {a}");
    //    // 나중에 이것을 사운드로 대체하거나 할 수 있다.

    //}

    void OnRunEvent(string a)
    {
        Debug.Log($"왼발! {a}");
        // 나중에 이것을 사운드로 대체하거나 할 수 있다.

    }

    void RightFoot()
    {
        Debug.Log("오른발!");
        

    }

    void UpdateIdle()
    {
        // 키를 움직이면 UpdateMoving이 실행되게 코드를 짜면된다.

        // 애니매이션
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("speed", 0);
        anim.SetFloat("jump", 0);
        anim.SetFloat("slide", 0);

    }

    void UpdateJump()
    {
        // 플레이해서 유니티짱의 y 축 포지션은 변화가 없음
        // 그렇다면 그냥 스페이스바 누르면 점프하게 만들어보자
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("jump", 1);
        anim.SetFloat("slide", 0);

        Debug.Log("점프했다!");
       
    }

    void UpdateSilding()
    {
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("slide", 1);
    }

    void Update()
    {
        //if(_moveToDest)
        //{

        //    // 잠시 주석처
        //    wait_run_ratio = Mathf.Lerp(wait_run_ratio, 1, 10.0f * Time.deltaTime);
        //    // wait_run_ratio값을 1로 일정시간 동안 옮겨주세요라는뜻, 뒤에 Time.deltaTime 부분은 자연스럽게 되도록 조절 해야함

        //    Vector3 dir = _destPos - transform.position;
        //    if (dir.magnitude < 0.00001f) // 도착했을때 정확하게 0이 안나온다 float끼리 뺼 때
        //    {
        //        _moveToDest = false;
        //    }
        //    else
        //    {
        //        float _moveDist = Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude);
        //        // _moveDist의 최소값은 0이고 최대값은 dir.magnitude실제 거리 이다.

        //        //float _moveDist = _speed * Time.deltaTime;
        //        //if (_moveDist >= dir.magnitude)
        //        //{
        //        //    _moveDist = dir.magnitude;
        //        //}
        //        transform.position += dir.normalized * _moveDist;
        //        // dir은 방향벡터이긴한데 방향이랑 크기 둘다 가지고잇으니까 normalized 를 통해서 크기가 1인 방향벡터로 바꿔준다.

        //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);

        //        //transform.LookAt(_destPos);
        //    }
        //}

        switch (_state)
        {
            case PlayerState.Die:
                UpdateDie();
                break;
            case PlayerState.Moving:
                UpdateMoving();
                break;
            case PlayerState.Idle:
                UpdateIdle();
                break;
            case PlayerState.Jump:
                UpdateJump();
                break;
            case PlayerState.Sliding:
                UpdateSilding();
                break;

        }


        //if (_moveToDest)
        //{
        //    wait_run_ratio = Mathf.Lerp(wait_run_ratio, 1, 10.0f * Time.deltaTime);
        //    // wait_run_ratio값을 1로 일정시간 동안 옮겨주세요라는뜻, 뒤에 Time.deltaTime 부분은 자연스럽게 되도록 조절 해야함

        //    Animator anim = GetComponent<Animator>();
        //    anim.SetFloat("wait_run_ratio", wait_run_ratio);
        //    anim.Play("WAIT_RUN");
        //}
        //else
        //{
        //    wait_run_ratio = Mathf.Lerp(wait_run_ratio, 0, 10.0f * Time.deltaTime);
        //    // 현재 값에서 0으로 점점 이동을 해야 움직이는 상태에서 멈추게 된다.

        //    Animator anim = GetComponent<Animator>();
        //    anim.SetFloat("wait_run_ratio", wait_run_ratio);
        //    anim.Play("WAIT_RUN");
        //}

        //Animator anim = GetComponent<Animator>();
        //// GetComponent사용하면 component 뺴올 수 있다.
        //anim.Play("RUN");
        //anim.Play("WAIT");
        // 일단 키보드로 움직이는 것은 생각 안하고 마우스로만 움직이는거 구현해보자



    }

    void OnMouseClicked(Define.MouseEvent evt)
    {
        if (_state == PlayerState.Die)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);
        
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("Wall")))
        {
            _destPos = hit.point;
            _state = PlayerState.Moving;
            
        }

    }

    void OnKeyBoardPressed(Define.KeyBoardEvent evt)
    {
        if (_state == PlayerState.Die)
            return;

        Debug.Log("들어옴");
        _state = PlayerState.Jump;
        
    }

   
}
