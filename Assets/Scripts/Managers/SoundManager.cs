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
    

    // ĳ�� ������ �� _audioClips ��Ǿ 
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

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

        GameObject cube = GameObject.Find("Cube");
        
        if(cube == null)
        {
            cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = new Vector3(0, 1, 3);
            cube.transform.name = "MyCube";
            cube.AddComponent<AudioSource>();


        }
    }
    
    public void Clear()
    {
        foreach(AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        _audioClips.Clear();
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

    }

    public void Play(AudioClip audioClip, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f) // path�� ��θ� �޾��ְ� pitch = �Ҹ� �ӵ� ����
    {
        if (audioClip == null)
        {
            return;
        }

        if (type == Define.Sound.Bgm)
        {
            AudioSource audioSource = _audioSources[(int)Define.Sound.Bgm];
            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.Play();
        }

        else // (type == Define.Sound.Effect)
        {
            AudioSource audioSource = _audioSources[(int)Define.Sound.Bgm];
            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.PlayOneShot(audioClip);
        }
    }

    // PlayMyCube�Լ����� ����� Ŭ���� ������ ���ش��� Mycube�� ���� �ٿ� ���� ����
    public void PlayMyCube(string clipPath)
    {
        GameObject cube = GameObject.Find("MyCube");
       
        if (clipPath.Contains("Sounds/") == false)
        {
            clipPath = $"Sounds/{clipPath}";
        }

        

        AudioClip myCubeClip  = Managers.Resource.Load<AudioClip>(clipPath); // ���ڷ� ���޹��� Clip���� ���⿡ Clip�� �ֽ�.
        if(myCubeClip == null)
        {
            Debug.Log($"MyCubeClip is Miissing! {clipPath}");
            return;
        }
        else
        {
           AudioSource cubeAudioSource = cube.GetComponent<AudioSource>();
            cubeAudioSource.maxDistance = 30.0f;
            cubeAudioSource.minDistance = 1.0f;
            cubeAudioSource.spatialBlend = 1.0f;

        }


    }

    AudioClip GetOrAddAudioClip(string path) // audioClip ��ȯ�ϴ� �Լ�(���� Dictionary���� �κп���)
    {
        AudioClip audioClip = null;
        if (_audioClips.TryGetValue(path, out audioClip) == false) // ������ �̷��� ���� ����ְ� 
        {
            // ���ٸ� ������ ����������
            audioClip = Managers.Resource.Load<AudioClip>(path);
            _audioClips.Add(path, audioClip);
        }

        return audioClip;
    }

}
