using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    class Test
    {
        public int id = 0;
    }

    // Coroutine을 정리하자면은
    // 1. 함수의 상태를 저장or복원이 가능!
    // 그러면 언제 쓰는것인가?
        // -> 엄청 오래 걸리는 작업을 끊거나
        // -> 원하는 타이밍에 함수를 잠시 Stop/복원하는 경우   이런 경우에 사용하면 좋다.
    // 2. return -> 우리가 원하는 타입으로 가능 (class도 가능)
    class CoroutineTest : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            for(int i = 0; i< 1000000; i++)
            {
                if(i % 10000 == 0)
                    yield return null;
            }
        }

        void GenerateItem()
        {
            // 아이템을 만들어준다.
            // DB저장

            // 멈춤
            // 로직
        }
    } 

    protected override void init()
    { 
        base.init();

        SceneType = Define.Scene.Game;

        Managers.UI.ShowSceneUI<UI_Inven>();

        CoroutineTest test = new CoroutineTest();

        //foreach(System.Object t in test)
        //{
        //    Test value = (Test)t;
        //    Debug.Log(value.id);
        //}
    }


    public override void Clear()
    {
        
    }


    
}
