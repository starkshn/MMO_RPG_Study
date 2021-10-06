using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    // MP3 Player -> AudioSource
    // MP3 ���� -> AudioClip
    // ����(��) -> AudioListener

    //AudioSource audioSource = new AudioSource(); ���� �����Ѱ�
    AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCount]; // �뵵�� ����� ����� ����.



    public void Play(Define.Sound type, string path, float pitch = 1.0f) // path�� ��θ� �޾��ְ� pitch = �Ҹ� �ӵ� ����
    {
        if (path.Contains("Sounds/") == false)
            path = $"Sounds/{path}";
        // �̷��� �ϸ� UnityChan/���� ���� �̷������� path�ִ´�.

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
                // ã�� ���ϱ� audioClip�� ��������ָ� �ȴ�.
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

            AudioSource audioSource = _audioSources[(int)Define.Sound.Effect]; // Effect���� ��쿡�� PlayOneshot���� ��� ������ �ƴϱ� source�� ��Ƴ���.
            audioSource.pitch = pitch;
            
            audioSource.PlayOneShot(audioClip); // Clip ���� ���� ������ ã���� clip�� �־����

        }
        // Ȥ�ö� ���� Ÿ���� �߰��� �ȴٸ� ���� if������ �� ó���� �ϸ�� ���̴�.
    }
}
