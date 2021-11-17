using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    Coroutine co;

    protected override void init()
    { 
        base.init();

        SceneType = Define.Scene.Game;

        Managers.UI.ShowSceneUI<UI_Inven>();

        co = StartCoroutine("CoExplodeAfter4Seconds", 4.0f);

        StartCoroutine("CoStopExplod", 2.0f);
         

    }

    IEnumerator CoStopExplod(float seconds)
    {
        Debug.Log("Stop Enter!");
        yield return new WaitForSeconds(seconds);
        Debug.Log("Stop Execute!!!!");
        if(co != null)
        {
            StopCoroutine(co);
            co = null;
        }
    }
     
    IEnumerator CoExplodeAfter4Seconds(float seconds)
    {
        Debug.Log("Explode Enter!");
        yield return new WaitForSeconds(seconds);
        Debug.Log("Explode Execute!!!!");
        co = null;
    }


    public override void Clear()
    {
        
    }


    
}
