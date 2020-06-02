using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoundGroup
{
    public string name;
    [SerializeField] private Sound[] sounds;

    public void Init(GameObject parent)
    {
        foreach (Sound s in sounds)
        {
            s.InitAudioSource(parent);
        }
    }

    public Sound GetSound(string soundName)
    {
        return Array.Find(sounds, sound => sound.name == soundName);
    }

    public void SetVolume(float volume)
    {
        foreach (Sound s in sounds)
        {
            s.SetVolume(volume);
        }
    }

    public void SetVolume(float volume, MonoBehaviour parent, float time)
    {
        foreach (Sound s in sounds)
        {
            s.SetVolume(volume, parent, time);
        }
    }

    public void SetSoundVolume(string soundName, float volume)
    {
        Sound s = GetSound(soundName);
        if (s == null)
        {
            Debug.LogWarning("Sound " + soundName + " not found!");
            return;
        }
        s.SetVolume(volume);
    }

    public void SetSoundVolume(string soundName, float volume, MonoBehaviour parent, float time)
    {
        Sound s = GetSound(soundName);
        if (s == null)
        {
            Debug.LogWarning("Sound " + soundName + " not found!");
            return;
        }
        s.SetVolume(volume, parent, time);
    }

    public void Stop()
    {
        foreach (Sound s in sounds)
        {
            s.Stop();
        }
    }

    public void Stop(MonoBehaviour parent, float time)
    {
        foreach (Sound s in sounds)
        {
            s.Stop(parent, time);
        }
    }
}
