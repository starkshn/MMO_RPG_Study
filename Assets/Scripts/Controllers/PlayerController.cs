using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public enum PlayerState
    {
        Die,
        Moving,
        Idle,
        Jump,
        Sliding,
        Skill,
    }

    int _mask = (1 << (int)Define.Layer.Ground | (1 << (int)Define.Layer.Monster));

    PlayerStat _stat;
    Vector3 _destPos;

    InputManager isSpace = new InputManager();

    [SerializeField]
    PlayerState _state = PlayerState.Idle;

    GameObject _lockTarget;

    public PlayerState State
    {
        get { return _state; }

        set
        {
            _state = value;

            Animator anim = GetComponent<Animator>();

            switch (_state)
            {
                case PlayerState.Die:
                    break;
                case PlayerState.Idle:
                    anim.CrossFade("WAIT", 0.1f);
                    // anim.SetInt("state", (int)_state);
                    break;
                case PlayerState.Moving:
                    anim.CrossFade("RUN", 0.1f);
                    break;
                case PlayerState.Skill:
                    anim.CrossFade("ATTACK", 0.1f, -1, 0);
                    break;
            }
        }
    }

    void Start()
    {
        _stat = gameObject.GetComponent<PlayerStat>();

        Managers.Input.MouseAction -= OnMouseEvent;
        Managers.Input.MouseAction += OnMouseEvent;

        Managers.Input.KeyBoardAction -= OnKeyBoardPressed;
        Managers.Input.KeyBoardAction += OnKeyBoardPressed;

    } 

    void UpdateDie()
    {
        // 아무것도 못함
    }

    
    void UpdateMoving()
    {
        // 몬스터가 사정거리 범위 안에들어오면 공격
        if(_lockTarget != null)
        {
            float distance = (_destPos - transform.position).magnitude;
            if(distance <= 1)
            {
                State = PlayerState.Skill;
                return;
            }
        }

        // 이동하는 부분
        Vector3 dir = _destPos - transform.position;
        if (dir.magnitude < 0.1f)
        {
            //_moveToDest = false; 목적지에 도달한 상태이니까
            State = PlayerState.Idle;

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
                if(Input.GetMouseButton(0) == false) // 마우스를 누르고 있다.
                    State = PlayerState.Idle;
                return;
            }

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);
        }

        // 애니매이션
        // wait_run_ratio값을 1로 일정시간 동안 옮겨주세요라는뜻, 뒤에 Time.deltaTime 부분은 자연스럽게 되도록 조절 해야함

        //Animator anim = GetComponent<Animator>();
        //// 현재 게임상태에 대한 정보를 넘겨준다.
        //anim.SetFloat("speed", _stat.MoveSpeed);

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

    //void OnRunEvent(string a)
    //{
    //    //Debug.Log($"왼발! {a}");
    //    // 나중에 이것을 사운드로 대체하거나 할 수 있다.
    //}

    //void RightFoot()
    //{
    //    //Debug.Log("오른발!");
       
    //}

    void UpdateIdle()
    {
        // 키를 움직이면 UpdateMoving이 실행되게 코드를 짜면된다.

        // 애니매이션
        //Animator anim = GetComponent<Animator>();
        //anim.SetFloat("speed", 0);
        //anim.SetFloat("jump", 0);
        //anim.SetFloat("slide", 0);

    }

    //void UpdateJump()
    //{
    //    Animator anim = GetComponent<Animator>();
    //    anim.SetFloat("jump", 1);
    //    anim.SetFloat("slide", 0);
       
    //}

    //void UpdateSilding()
    //{
    //    Animator anim = GetComponent<Animator>();
    //    anim.SetFloat("slide", 1);
    //}

    void UpdateSkill()
    {
        if(_lockTarget != null)
        {
            Vector3 dir = _lockTarget.transform.position - transform.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);
        }
    }

    void OnHitEvent()
    {
        Debug.Log("OnHitEvent!");

        //TODO
        if(_stopSkill)
        {
            State = PlayerState.Idle;
        }
        else
        {
            State = PlayerState.Skill;
        }
    }

    void Update()
    {
        switch (State)
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
            //case PlayerState.Jump:
            //    UpdateJump();
            //    break;
            //case PlayerState.Sliding:
            //    UpdateSilding();
            //    break;
            case PlayerState.Skill:
                UpdateSkill();
                break;

        }

    }

    bool _stopSkill = false;
    void OnMouseEvent(Define.MouseEvent evt)
    {
        switch(State)
        {
            case PlayerState.Idle:
                OnMouseEvent_IdleRun(evt);
                break;
            case PlayerState.Moving:
                OnMouseEvent_IdleRun(evt);
                break;
            case PlayerState.Skill:
                {
                    if(evt == Define.MouseEvent.PointerUp)
                        _stopSkill = true;
                }
                break;
        }
    }


   void OnMouseEvent_IdleRun(Define.MouseEvent evt)
    {
        if (State == PlayerState.Die)
            return;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
         
        bool raycastHit = Physics.Raycast(ray, out hit, 100.0f, _mask);
        //Debug.DrawR  ay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

        switch (evt)
        {
            // PointerDown은 유니티의 Input.GetMouseButtonDown(0)에 대응을 한다.
            case Define.MouseEvent.PointerDown:
                {
                    if (raycastHit)
                    {
                        _destPos = hit.point;
                        _destPos.y = 0;

                        State = PlayerState.Moving;
                        _stopSkill = false;

                        if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
                            _lockTarget = hit.collider.gameObject;

                        else
                            _lockTarget = null;
                    }
                }
                break;

            // Press는  유니티의 Input.GetMouseButton(0)에 대응을 한다.
            case Define.MouseEvent.Press:
                {
                    if (_lockTarget == null && raycastHit)
                        _destPos = hit.point;
                }
                break;

            // PointerUp == Input.GetMouseButtonUp(0)
            // case Define.MouseEvent.ButtonUp(0)에서 _lockTarget = null로 해주니까 한번 클릭시 로그가 안찍힌다.
            //case Define.MouseEvent.PointerUp:
            //    _lockTarget = null;
            //    break;

            // 공격#2 에서 PointerUp일 경우 _stopSkill 
            case Define.MouseEvent.PointerUp:
                _stopSkill = true;
                break;
        }
    }

    void OnKeyBoardPressed(Define.KeyBoardEvent evt)
    {
        if (State == PlayerState.Die)
            return;

        Debug.Log("들어옴");
        State = PlayerState.Jump;

    }
}
