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
        
    }

    public AudioClip audioClip1;

    private void OnTriggerEnter(Collider other)
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.PlayOneShot(audioClip1);
    }
}
