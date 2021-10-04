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

    public AudioClip audioClip; // ���⿡ audioclip2���� �־��� �����̴�.
    public AudioClip audioClip2;

    private void OnTriggerEnter(Collider other)
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.PlayOneShot(audioClip);
        audio.PlayOneShot(audioClip2);

        float lifeTime = Mathf.Max(audioClip.length, audioClip2.length);

        GameObject.Destroy(gameObject, lifeTime);
    }
}
