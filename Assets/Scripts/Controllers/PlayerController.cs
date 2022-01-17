using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : BaseController
{
    int _mask = (1 << (int)Define.Layer.Ground | (1 << (int)Define.Layer.Monster));

    InputManager isSpace = new InputManager();

    PlayerStat _stat;

    bool _stopSkill = false;

    public override void init()
    {

        WorldObjectType = Define.WorldObject.Player;

        _stat = gameObject.GetComponent<PlayerStat>();

        Managers.Input.MouseAction -= OnMouseEvent; 
        Managers.Input.MouseAction += OnMouseEvent;

        Managers.Input.KeyBoardAction -= OnKeyBoardPressed;
        Managers.Input.KeyBoardAction += OnKeyBoardPressed;

        if (gameObject.GetComponentInChildren<UI_HPBar>() == null)
            Managers.UI.Make3D_UI<UI_HPBar>(transform);

    }

    protected override void UpdateMoving()
    {
        // 몬스터가 사정거리 범위 안에들어오면 공격
        if(_lockTarget != null)
        {
            float distance = (_destPos - transform.position).magnitude;
            if(distance <= 1)
            {
                State = Define.State.Skill;
                return;
            }
        }

        // 이동하는 부분
        Vector3 dir = _destPos - transform.position;
        if (dir.magnitude < 0.1f)
        {
            //_moveToDest = false; 목적지에 도달한 상태이니까
            State = Define.State.Idle;

        }
        else
        {
            // ToDo
            //NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();
            //float _moveDist = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude);
            ////nma.CalculatePath (애도 굉장히 중요하게 사용이 될 거 같다. => 나중에 몬스터)
            //nma.Move(dir.normalized * _moveDist);
            // TODO 부터 지금 NVM 를 사용해서 Move하니까 몬스터가 밀쳐지는데
            // 원래 처음에 했던 방식으로 하면 

            Debug.DrawRay(transform.position + Vector3.up * 0.5f, dir.normalized, Color.green);
            if (Physics.Raycast(transform.position + Vector3.up * 0.5f , dir, 1.0f, LayerMask.GetMask("Block")))
            {
                if(Input.GetMouseButton(0) == false) // 마우스를 누르고 있다.
                    State = Define.State.Idle;
                return;
            }

            float _moveDist = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude);
            transform.position += dir.normalized * _moveDist;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);
        }
    }

    protected override void UpdateSkill()
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
        if (_lockTarget != null)
        {
            Stat targetStat = _lockTarget.GetComponent<Stat>();
            targetStat.OnAttacked(_stat);

            //PlayerStat myStat = transform.GetComponent<PlayerStat>();
            //int damage = Mathf.Max(0, myStat.Attack - targetStat.Defense);
            //targetStat.Hp -= damage;
            // 위에 세줄은 Stat에서 함수로 구현을 해놓은거임 == OnAttacked()함수
        }
        
        //TODO
        if(_stopSkill)
        {
            State = Define.State.Idle;
        }
        else
        {
            State = Define.State.Skill;
        }
    }

    void OnMouseEvent(Define.MouseEvent evt)
    {
        switch(State)
        {
            case Define.State.Idle:
                OnMouseEvent_IdleRun(evt);
                break;
            case Define.State.Moving:
                OnMouseEvent_IdleRun(evt);
                break;
            case Define.State.Skill:
                {
                    if(evt == Define.MouseEvent.PointerUp)
                        _stopSkill = true;
                }
                break;
        }
    }
   void OnMouseEvent_IdleRun(Define.MouseEvent evt)
    {
        if (State == Define.State.Die)
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

                        State = Define.State.Moving;
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
        if (State == Define.State.Die)
            return;

        Debug.Log("들어옴");
        State = Define.State.Jump;
    }
}
