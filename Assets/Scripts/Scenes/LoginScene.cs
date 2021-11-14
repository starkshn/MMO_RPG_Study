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

        List<GameObject> list = new List<GameObject>();
        for(int i = 0; i < 5; i++)
            list.Add(Managers.Resource.Instantiate("UnityChan"));

        foreach(GameObject obj in list)
        {
            Managers.Resource.Destroy(obj);
        }
            


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
