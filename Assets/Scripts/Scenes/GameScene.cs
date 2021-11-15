using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    class CoroutineTest : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return 1;
            yield return 2;
            yield return 3;
            yield return 4;
        }
    } 

     

    protected override void init()
    { 
        base.init();

        SceneType = Define.Scene.Game;

        Managers.UI.ShowSceneUI<UI_Inven>();

        CoroutineTest test = new CoroutineTest();

        foreach(int t in test)
        {
            Debug.Log(t);
        }
    }


    public override void Clear()
    {
        
    }


    
}
