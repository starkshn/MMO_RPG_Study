using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_HPBar : UI_Base
{

    enum GameObjects
    {
        HPBar,
    }
    public override void init()
    {
        Bind<GameObject>(typeof(GameObjects));
    }
}
