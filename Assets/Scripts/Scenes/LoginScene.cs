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

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene("Game");
            SceneManager.LoadSceneAsync
        }
    }

    public override void Clear()
    {
        
    }
}
