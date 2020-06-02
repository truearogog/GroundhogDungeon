using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private SoundGroup[] soundGroups;

    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (SoundGroup s in soundGroups)
        {
            s.Init(gameObject);
        }
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0)
        {
            SetSoundVolume("Music", "Theme", 0.1f);
            Play("Music", "Theme");
        }
        if (scene.buildIndex == 2)
        {
            SetSoundVolume("Music", "Theme", 0.15f);
        }
    }

    public void Play(string soundGroupName, string soundName)
    {
        SoundGroup sg = Array.Find(soundGroups, soundGroup => soundGroup.name == soundGroupName);
        if (sg == null)
        {
            Debug.LogWarning("Sound group " + soundGroupName + " not found!");
            return;
        }
        Sound s = sg.GetSound(soundName);
        if (s == null)
        {
            Debug.LogWarning("Sound " + soundName + " not found!");
            return;
        }
        s.Play();
    }

    public void SetSoundGroupVolume(string soundGroupName, float volume)
    {
        SoundGroup sg = Array.Find(soundGroups, soundGroup => soundGroup.name == soundGroupName);
        if (sg == null)
        {
            Debug.LogWarning("Sound group " + soundGroupName + " not found!");
            return;
        }
        sg.SetVolume(volume);
    }

    public void SetSoundGroupVolume(string soundGroupName, float volume, float time)
    {
        SoundGroup sg = Array.Find(soundGroups, soundGroup => soundGroup.name == soundGroupName);
        if (sg == null)
        {
            Debug.LogWarning("Sound group " + soundGroupName + " not found!");
            return;
        }
        sg.SetVolume(volume, this, time);
    }

    public void SetSoundVolume(string soundGroupName, string soundName, float volume)
    {
        SoundGroup sg = Array.Find(soundGroups, soundGroup => soundGroup.name == soundGroupName);
        if (sg == null)
        {
            Debug.LogWarning("Sound group " + soundGroupName + " not found!");
            return;
        }
        sg.SetSoundVolume(soundName, volume);
    }

    public void SetSoundVolume(string soundGroupName, string soundName, float volume, float time)
    {
        SoundGroup sg = Array.Find(soundGroups, soundGroup => soundGroup.name == soundGroupName);
        if (sg == null)
        {
            Debug.LogWarning("Sound group " + soundGroupName + " not found!");
            return;
        }
        sg.SetSoundVolume(soundName, volume, this, time);
    }

    public void SetSoundLoop(string soundGroupName, string soundName, bool loop)
    {
        SoundGroup sg = Array.Find(soundGroups, soundGroup => soundGroup.name == soundGroupName);
        if (sg == null)
        {
            Debug.LogWarning("Sound group " + soundGroupName + " not found!");
            return;
        }
        Sound s = sg.GetSound(soundName);
        if (s == null)
        {
            Debug.LogWarning("Sound " + soundName + " not found!");
            return;
        }
        s.loop = loop;
    }

    public void Stop(string soundGroupName)
    {
        SoundGroup sg = Array.Find(soundGroups, soundGroup => soundGroup.name == soundGroupName);
        if (sg == null)
        {
            Debug.LogWarning("Sound group " + soundGroupName + " not found!");
            return;
        }
        sg.Stop();
    }

    public void Stop(string soundGroupName, float time)
    {
        SoundGroup sg = Array.Find(soundGroups, soundGroup => soundGroup.name == soundGroupName);
        if (sg == null)
        {
            Debug.LogWarning("Sound group " + soundGroupName + " not found!");
            return;
        }
        sg.Stop(this, time);
    }

    public void StopSound(string soundGroupName, string soundName)
    {
        SoundGroup sg = Array.Find(soundGroups, soundGroup => soundGroup.name == soundGroupName);
        if (sg == null)
        {
            Debug.LogWarning("Sound group " + soundGroupName + " not found!");
            return;
        }
        Sound s = sg.GetSound(soundName);
        if (s == null)
        {
            Debug.LogWarning("Sound " + soundName + " not found!");
            return;
        }
        s.Stop();
    }

    public void StopSound(string soundGroupName, string soundName, float time)
    {
        SoundGroup sg = Array.Find(soundGroups, soundGroup => soundGroup.name == soundGroupName);
        if (sg == null)
        {
            Debug.LogWarning("Sound group " + soundGroupName + " not found!");
            return;
        }
        Sound s = sg.GetSound(soundName);
        if (s == null)
        {
            Debug.LogWarning("Sound " + soundName + " not found!");
            return;
        }
        s.Stop(this, time);
    }
}
