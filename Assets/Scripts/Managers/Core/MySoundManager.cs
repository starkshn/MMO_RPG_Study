using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySoundManager
{
    // MP3 Player -> AudioSource
    // MP3 음원 -> AudioClip
    // 관객(귀) -> AudioListener

    AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCount]; // 용도를 나누어서 만들어 놓자.


    // 캐싱 역할을 할 _audioClips 딕션어리 
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

    public void init()
    {
        GameObject cube = GameObject.Find("Cube");
        if (cube == null)
        {
            cube = new GameObject { name = "Cube" };
            Object.DontDestroyOnLoad(cube);

            string[] soundNames = System.Enum.GetNames(typeof(Define.Sound));

            for (int i = 0; i < soundNames.Length - 1; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                _audioSources[i] = go.AddComponent<AudioSource>(); 
                go.transform.parent = cube.transform;
            }

            _audioSources[(int)Define.Sound.Bgm].loop = true;
        }
    }

    public void Clear()
    {
        foreach (AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        _audioClips.Clear();
    }

    public void Play(string path, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f) 
    {
        if (path.Contains("Sounds/") == false)
        {
            path = $"Sounds/{path}";
            // 이렇게 하면 UnityChan/사운드 몇몇번 이런식으로 path넣는다.
        }

        if (type == Define.Sound.Bgm)
        {
            AudioClip audioClip = Managers.Resource.Load<AudioClip>(path);
            if (audioClip == null)
            {
                Debug.Log($"AudioClip Missing ! {path}");
                return;
            }

            // ToDo (bgm 처리부분)
            AudioSource audioSource = _audioSources[(int)Define.Sound.Bgm];
            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.Play();


        }

        else if (type == Define.Sound.Effect)
        {
            AudioClip audioClip = Managers.Resource.Load<AudioClip>(path);
            if (audioClip == null)
            {
                Debug.Log($"AudioClip Missing ! {path}");
                return;
            }

            AudioSource audioSource = _audioSources[(int)Define.Sound.Effect]; // Effect같은 경우에는 PlayOneshot으로 사용 할지를 아니까 audioSource에 담아놓자.
            audioSource.pitch = pitch;

            audioSource.PlayOneShot(audioClip); // Clip 같은 경우는 위에서 찾아준 clip을 넣어놓자

        }

        // 점프하는 사운드 구현
        else
        {
            AudioClip audioClip = GetOrAddAudioClip(path);
            if (audioClip == null)
            {
                Debug.Log($"AudioClip Missing ! {path}");
                return;
            }

            AudioSource audioSource = _audioSources[(int)Define.Sound.Jump];

            audioSource.pitch = pitch;

            audioSource.PlayOneShot(audioClip);
        }
        // 혹시라도 사운드 타입이 추가가 된다면 여기 if문에서 잘 처리를 하면될 것이다.
    }

    AudioClip GetOrAddAudioClip(string path) // audioClip 반환하는 함수(위에 Dictionary만든 부분에서)
    {
        AudioClip audioClip = null;
        if (_audioClips.TryGetValue(path, out audioClip) == false) // 있으면 이렇게 값을 뱉어주고 
        {
            // 없다면 이전과 마찬가지로
            audioClip = Managers.Resource.Load<AudioClip>(path);
            _audioClips.Add(path, audioClip);
        }

        return audioClip;
    }

}
