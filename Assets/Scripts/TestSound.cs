using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSound : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public AudioClip audioClip; // 여기에 audioclip2번에 넣어진 상태이다. -> 유니티에서 드래그드롭으로 작업함.
    public AudioClip audioClip2;

    private void OnTriggerEnter(Collider other)
    {
        //AudioSource audio = GetComponent<AudioSource>();
        //audio.PlayOneShot(audioClip);
        //audio.PlayOneShot(audioClip2);
        //float lifeTime = Mathf.Max(audioClip.length, audioClip2.length);
        //GameObject.Destroy(gameObject, lifeTime

        Managers.Sound.Play(Define.Sound.Effect, "UnityChan/univ0001"); // 두번째 인자에 audioClip을 넣는것이 아니라 이제는 경로를 넣어준다.
        Managers.Sound.Play(Define.Sound.Effect, "UnityChan/univ0002"); 

    }
}
