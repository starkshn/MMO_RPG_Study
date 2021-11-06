using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginScene : BaseScene
{

    protected override void init()
    {
        base.init();

        SceneType = Define.Scene.Login;

        for(int i = 0; i < 10; i++)
            Managers.Resource.Instantiate("UnityChan");


    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            //SceneManager.LoadScene("Game"); �� ������ �ƴ϶� ������ SceneMagerEx�� ���� ���� ����Ұ��̴�.
            Managers.Scene.LoadScene(Define.Scene.Game);
            
        }
    }

    public override void Clear()
    {
        Debug.Log("Login Scene Clear!");
    }
}
