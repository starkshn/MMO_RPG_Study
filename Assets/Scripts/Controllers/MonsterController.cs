using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : BaseController
{

    Stat _stat;

    [SerializeField]
    float _scanRange = 8.0f;

    [SerializeField]
    float _attackRange = 2.0f;


    public override void init()
    {
        _stat = gameObject.GetComponent<Stat>();

        if(gameObject.GetComponentInChildren<UI_HPBar>() == null)
            Managers.UI.Make3D_UI<UI_HPBar>(transform);
    }

    protected override void UpdateIdle()
    {
        base.UpdateIdle();
        Debug.Log("monster UpdateIdle!");


        // TODO : 매니저가 생기면 옮기자.
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
            return;

        float distance = (player.transform.position - transform.position).magnitude;
        
        if(distance <= _scanRange)
        {
            Debug.Log("Find!");
            _lockTarget = player;
            State = Define.State.Moving;
            return;
        }
    }

    protected override void UpdateMoving()
    {
        Debug.Log("monster UpdateMoving!!");
    }

    protected override void UpdateSkill()
    {
        Debug.Log("monster UpdateSkill!!");
    }

    void OnHitEvent()
    {
        Debug.Log("monster OnHitEvent!");
    }
}
