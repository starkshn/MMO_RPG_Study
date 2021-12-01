using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _speed = 10.0f;

    Vector3 _destPos;

    InputManager isSpace = new InputManager();
    //bool Jump = false;
    
    void Start()
    {
        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;

        Managers.Input.KeyBoardAction -= OnKeyBoardPressed;
        Managers.Input.KeyBoardAction += OnKeyBoardPressed;

    }

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
        if (dir.magnitude < 0.1f)
        {
            //_moveToDest = false; 목적지에 도달한 상태이니까
            _state = PlayerState.Idle;

        }
        else
        {
            // ToDo
            NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();

            float _moveDist = Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude);
            //nma.CalculatePath (애도 굉장히 중요하게 사용이 될 거 같다.)
            nma.Move(dir.normalized * _moveDist); 

            //transform.position += dir.normalized * _moveDist;
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
       
    }

    void UpdateSilding()
    {
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("slide", 1);
    }

    void Update()
    {
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
