using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    // MP3 Player -> AudioSource
    // MP3 음원 -> AudioClip
    // 관객(귀) -> AudioListener

    //AudioSource audioSource = new AudioSource(); 내가 선언한것
    AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCount]; // 용도를 나누어서 만들어 놓자.



    public void Play(Define.Sound type, string path, float pitch = 1.0f) // path로 경로를 받아주고 pitch = 소리 속도 조절
    {
        if (path.Contains("Sounds/") == false)
            path = $"Sounds/{path}";
        // 이렇게 하면 UnityChan/사운드 몇몇번 이런식으로 path넣는다.

        if(type == Define.Sound.Bgm)
        {
            AudioClip audioClip = Managers.Resource.Load<AudioClip>(path);
            if(audioClip == null)
            {
                Debug.Log($"AudioClip Missing ! {path}");
                return;
            }

            // ToDo
            else
            {
                // 찾은 경우니까 audioClip을 재생시켜주면 된다.
            }
        }
        else
        {
            AudioClip audioClip = Managers.Resource.Load<AudioClip>(path);
            if (audioClip == null)
            {
                Debug.Log($"AudioClip Missing ! {path}");
                return;
            }

            AudioSource audioSource = _audioSources[(int)Define.Sound.Effect]; // Effect같은 경우에는 PlayOneshot으로 사용 할지를 아니까 source에 담아놓자.
            audioSource.pitch = pitch;
            
            audioSource.PlayOneShot(audioClip); // Clip 같은 경우는 위에서 찾아준 clip을 넣어놓자

        }
        // 혹시라도 사운드 타입이 추가가 된다면 여기 if문에서 잘 처리를 하면될 것이다.
    }
}
