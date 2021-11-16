using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    class Test
    {
        public int id = 0;
    }

    // Coroutine�� �������ڸ���
    // 1. �Լ��� ���¸� ����or������ ����!
    // �׷��� ���� ���°��ΰ�?
        // -> ��û ���� �ɸ��� �۾��� ���ų�
        // -> ���ϴ� Ÿ�ֿ̹� �Լ��� ��� Stop/�����ϴ� ���   �̷� ��쿡 ����ϸ� ����.
    // 2. return -> �츮�� ���ϴ� Ÿ������ ���� (class�� ����)
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
            // �������� ������ش�.
            // DB����

            // ����
            // ����
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
