using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    PlayerStat _stat;
    Vector3 _destPos;
    InputManager isSpace = new InputManager();

    Texture2D _attackIcon;
    Texture2D _handIcon;

    enum CursorType
    {
        None,
        Attack,
        Hand,
    }

    CursorType _cursorType = CursorType.None;

    void Start()
    {
        _attackIcon = Managers.Resource.Load<Texture2D>("Textures/Cursor/Attack");
        _handIcon = Managers.Resource.Load<Texture2D>("Textures/Cursor/Hand");

        _stat = gameObject.GetComponent<PlayerStat>();

        Managers.Input.MouseAction -= OnMouseEvent;
        Managers.Input.MouseAction += OnMouseEvent;

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
        Skill,
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

            float _moveDist = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude);
            //nma.CalculatePath (애도 굉장히 중요하게 사용이 될 거 같다. => 나중에 몬스터)
            nma.Move(dir.normalized * _moveDist);

            Debug.DrawRay(transform.position + Vector3.up * 0.5f, dir.normalized, Color.green);
            if (Physics.Raycast(transform.position + Vector3.up * 0.5f , dir, 1.0f, LayerMask.GetMask("Block")))
            {
                _state = PlayerState.Idle;
                return;
            }

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);
        }
        // 애니매이션
        // wait_run_ratio값을 1로 일정시간 동안 옮겨주세요라는뜻, 뒤에 Time.deltaTime 부분은 자연스럽게 되도록 조절 해야함
        Animator anim = GetComponent<Animator>();
        // 현재 게임상태에 대한 정보를 넘겨준다.
        anim.SetFloat("speed", _stat.MoveSpeed);

    }

    //void GetSlide()
    //{
    //    if(Input.GetKeyDown(KeyCode.A))
    //    {
    //        _state = PlayerState.Sliding;
    //    }
    //   if(_state == PlayerState.Moving)
    //    {
    //        _state = PlayerState.Moving;
    //    }
    //}

    //void OnRunEvent(int a)
    //{
    //    Debug.Log($"왼발! {a}");
    //    // 나중에 이것을 사운드로 대체하거나 할 수 있다.

    //}

    void OnRunEvent(string a)
    {
        //Debug.Log($"왼발! {a}");
        // 나중에 이것을 사운드로 대체하거나 할 수 있다.
    }

    void RightFoot()
    {
        //Debug.Log("오른발!");
       
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

        UpdateMouseCursor();

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

    void UpdateMouseCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100.0f, _mask))
        {
            if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
            {
                if(_cursorType != CursorType.Attack)
                {
                    Cursor.SetCursor(_attackIcon, new Vector2(_attackIcon.width / 5, 0), CursorMode.Auto);
                    _cursorType = CursorType.Attack;
                }
            }
            else
            {
                if(_cursorType != CursorType.Hand)
                {
                    Cursor.SetCursor(_handIcon, new Vector2(_handIcon.width / 3, 0), CursorMode.Auto);
                    _cursorType = CursorType.Hand;
                }
                
            }
        }
    }

    int _mask = (1 << (int)Define.Layer.Ground | (1 << (int)Define.Layer.Monster));

    GameObject _lockTarget;

    void OnMouseEvent(Define.MouseEvent evt)
    {
        if (_state == PlayerState.Die)
            return;

        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        bool raycastHit = Physics.Raycast(ray, out hit, 100.0f, _mask);
        //Debug.DrawR  ay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

        switch(evt)
        {
            case Define.MouseEvent.PointerDown:
                {
                    if(raycastHit)
                    {
                        _destPos = hit.point;
                        _state = PlayerState.Moving;

                        if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
                            _lockTarget = hit.collider.gameObject;

                        else
                            _lockTarget = null;
                    }
                }  
                break;

            case Define.MouseEvent.Press:
                {
                    if(_lockTarget != null)
                    {
                        _destPos = _lockTarget.transform.position;
                    }
                    else if(raycastHit)
                        _destPos = hit.point;
                    
                }
                break;

            case Define.MouseEvent.PointerUp:
                _lockTarget = null;
                break;
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
