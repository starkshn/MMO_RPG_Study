using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSound2 : MonoBehaviour
{

    void Start()
    {

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Managers.Sound.Play("UnityChan/univ0003", Define.Sound.Jump);
        }
    }

    public AudioClip audioClip; 
   
    int i = 0;
    private void OnTriggerEnter(Collider other)
    {
        i++;

        if (i % 2 == 0)
        {
            Managers.Sound.Play("UnityChan/univ0001", Define.Sound.Bgm); // 두번째 인자에 audioClip을 넣는것이 아니라 이제는 경로를 넣어준다.
        }
        else
        {
            Managers.Sound.Play("UnityChan/univ0002", Define.Sound.Bgm);
        }

        //else if(i == 2)
        //{
        //    Managers.Sound.Play("UnityChan/univ0012", Define.Sound.Bgm);
        //}

    }


}
