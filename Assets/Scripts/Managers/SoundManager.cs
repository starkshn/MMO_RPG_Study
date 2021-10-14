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

    public void init()
    {
        GameObject root = GameObject.Find("@Sound");
        if(root == null)
        {
            root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(root);

            string[] soundNames = System.Enum.GetNames(typeof(Define.Sound));

            for(int i = 0; i < soundNames.Length - 1; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                _audioSources[i] = go.AddComponent<AudioSource>(); // ������ ���� _audioSources�� �־��ش�.
                go.transform.parent = root.transform;
            }
            // soundName�� ���鼭 ���ο� GameObject�� ������ش�.

            _audioSources[(int)Define.Sound.Bgm].loop = true; // Bgm���� ��쿡�� ������ ��� ���尡 ������ ���ش�.
        }
    }

    public void Play(string path, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f) // path�� ��θ� �޾��ְ� pitch = �Ҹ� �ӵ� ����
    {
        if (path.Contains("Sounds/") == false)
        {
            path = $"Sounds/{path}";
        // �̷��� �ϸ� UnityChan/���� ���� �̷������� path�ִ´�.
        }

        if(type == Define.Sound.Bgm)
        {
            AudioClip audioClip = Managers.Resource.Load<AudioClip>(path);
            if(audioClip == null)
            {
                Debug.Log($"AudioClip Missing ! {path}");
                return;
            }

            // ToDo (bgm ó���κ�)
            AudioSource audioSource = _audioSources[(int)Define.Sound.Bgm];
            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.Play();

            
        }

        else if(type == Define.Sound.Effect)
        {
            AudioClip audioClip = Managers.Resource.Load<AudioClip>(path);
            if (audioClip == null)
            {
                Debug.Log($"AudioClip Missing ! {path}");
                return;
            }

            AudioSource audioSource = _audioSources[(int)Define.Sound.Effect]; // Effect���� ��쿡�� PlayOneshot���� ��� ������ �ƴϱ� audioSource�� ��Ƴ���.
            audioSource.pitch = pitch;
       
            audioSource.PlayOneShot(audioClip); // Clip ���� ���� ������ ã���� clip�� �־����

        }

        // �����ϴ� ���� ����
        else
        {
            AudioClip audioClip = Managers.Resource.Load<AudioClip>(path);
            if (audioClip == null)
            {
                Debug.Log($"AudioClip Missing ! {path}");
                return;
            }

            AudioSource audioSource = _audioSources[(int)Define.Sound.Jump];

            audioSource.pitch = pitch;

            audioSource.PlayOneShot(audioClip);
        }
        // Ȥ�ö� ���� Ÿ���� �߰��� �ȴٸ� ���� if������ �� ó���� �ϸ�� ���̴�.
    }
}
