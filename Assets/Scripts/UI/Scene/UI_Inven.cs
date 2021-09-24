using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inven : UI_Scene
{

    enum GameObjects
    {
        GridPanel,

    }

    void Start()
    {
        
    }

    public override void init()
    {
        base.init();

        Bind<GameObject>(typeof(GameObjects));
    }

    
}
