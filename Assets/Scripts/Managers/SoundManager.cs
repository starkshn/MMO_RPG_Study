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
    

    // 캐싱 역할을 할 _audioClips 딕션어리 
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
                _audioSources[i] = go.AddComponent<AudioSource>(); // 위에서 만든 _audioSources에 넣어준다.
                go.transform.parent = root.transform;
            }
            // soundName을 돌면서 새로운 GameObject를 만들어준다.

            _audioSources[(int)Define.Sound.Bgm].loop = true; // Bgm같은 경우에는 루프로 계속 사운드가 나도록 해준다.
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

    public void Play(string path, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f) // path로 경로를 받아주고 pitch = 소리 속도 조절
    {
        if (path.Contains("Sounds/") == false)
        {
            path = $"Sounds/{path}";
        // 이렇게 하면 UnityChan/사운드 몇몇번 이런식으로 path넣는다.
        }

        if(type == Define.Sound.Bgm)
        {
            AudioClip audioClip = Managers.Resource.Load<AudioClip>(path);
            if(audioClip == null)
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

        else if(type == Define.Sound.Effect)
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

    }

    public void Play(AudioClip audioClip, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f) // path로 경로를 받아주고 pitch = 소리 속도 조절
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

    // PlayMyCube함수에는 오디오 클립만 전달을 해준다음 Mycube에 갖다 붙여 넣을 꺼임
    public void PlayMyCube(string clipPath)
    {
        GameObject cube = GameObject.Find("MyCube");
       
        if (clipPath.Contains("Sounds/") == false)
        {
            clipPath = $"Sounds/{clipPath}";
        }

        

        AudioClip myCubeClip  = Managers.Resource.Load<AudioClip>(clipPath); // 인자로 전달받은 Clip저장 여기에 Clip이 있슴.
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
