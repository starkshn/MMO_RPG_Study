using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSound2 : MonoBehaviour
{
    public AudioClip audioClip; 
   
    private void OnTriggerEnter(Collider other)
    {
        Managers.Sound.PlayMyCube("UnityChan/univ0012");

    }


}
