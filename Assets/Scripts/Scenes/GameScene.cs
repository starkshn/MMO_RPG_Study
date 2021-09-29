using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{

    public override void Clear()
    {
        init();
    }

    protected override void init()
    {
        base.init();

        SceneType = Define.Scene.Game;

        Managers.UI.ShowSceneUI<UI_Inven>();
    }

    void Start()
    {
        

    }

    
}
