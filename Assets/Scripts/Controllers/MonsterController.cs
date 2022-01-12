using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : BaseController
{
    Stat _stat;

    [SerializeField]
    float _scanRange = 8.0f;

    [SerializeField]
    float _attackRange = 1.0f;


    public override void init()
    {

        WorldObjectType = Define.WorldObject.Monster;

        _stat = gameObject.GetComponent<Stat>();

        if(gameObject.GetComponentInChildren<UI_HPBar>() == null)
            Managers.UI.Make3D_UI<UI_HPBar>(transform);
    }

    protected override void UpdateIdle()
    {
        base.UpdateIdle();
        
        // TODO : 매니저가 생기면 옮기자.
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
            return;

        float distance = (player.transform.position - transform.position).magnitude;
        
        if(distance <= _scanRange)
        {
            _lockTarget = player;
            State = Define.State.Moving;
            return;
        }
    }

    protected override void UpdateMoving()
    {

        _destPos = _lockTarget.transform.position;

        // 플레이어가 사정거리 범위 안에들어오면 공격
        if (_lockTarget != null)
        {
            float distance = (_destPos - transform.position).magnitude;
            if (distance <= _attackRange)
            {
                NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();
                nma.SetDestination(transform.position);
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
            NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();
            nma.SetDestination(_destPos);
            nma.speed = _stat.MoveSpeed * 0.8f;
            
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);
        }
    }

    protected override void UpdateSkill()
    {
    
        if (_lockTarget != null)
        {
            Vector3 dir = _lockTarget.transform.position - transform.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);
        }

    }

    void OnHitPlayerEvent()
    {
        Debug.Log("monster Attack Player!!!!!");

        if(_lockTarget != null)
        {
            // 체력 깍기
            Stat targetStat = _lockTarget.GetComponent<Stat>();
            int damage = Mathf.Max(0, _stat.Attack - targetStat.Defense);
            targetStat.Hp -= damage;

            if (targetStat.Hp <= 0)
                Managers.Game.Despawn(targetStat.gameObject);

            if(targetStat.Hp > 0)
            {
                float distance = (_lockTarget.transform.position - transform.position).magnitude;

                if (distance <= _attackRange)
                    State = Define.State.Skill;
                else
                    State = Define.State.Moving;
            }
            else
            {
                State = Define.State.Idle;
            }
        }
        else
        {
            State = Define.State.Idle;
        }
    }
}
