using UnityEngine.Audio;
using UnityEngine;
using System.Collections;

[System.Serializable]
public class Sound
{
    #region Variables

    #region Public Variables
    public string name;
    public bool loop;
    [HideInInspector]
    public AudioSource source;
    #endregion

    #region Private Variables
    [SerializeField] private AudioClip[] clips;

    [SerializeField] private float maxVolume;
    [SerializeField] private float volume;
    [Range(.1f, 3f)]
    [SerializeField] private float pitch;
    #endregion

    #endregion

    private float TranslateFloat(float x1, float min1, float max1, float min2, float max2)
    {
        float r1 = max1 - min1;
        float r2 = max2 - min2;
        return (r2 * x1 + min2 * r1) / r1;
    }
    //TranslateFloat(volume, 0f, 1f, 0f, maxVolume);

    public void InitAudioSource(GameObject parent)
    {
        source = parent.AddComponent<AudioSource>();
        source.volume = volume;
        source.pitch = pitch;
        source.loop = loop;
    }

    public void Play()
    {
        source.clip = clips[Random.Range(0, clips.Length)];
        source.Play();
    }

    public void SetVolume(float volume)
    {
        this.volume = volume;
        source.volume = this.volume;
    }

    public void SetVolume(float volume, MonoBehaviour parent, float time)
    {
        parent.StartCoroutine(SetSoundVolumeRoutine(volume, time));
    }

    private IEnumerator SetSoundVolumeRoutine(float volume, float setTime)
    {
        float initialVolume = source.volume;
        float time = 0;
        while (time <= setTime)
        {
            float currentDeltaTime = Time.deltaTime;
            time += Time.deltaTime;
            this.volume = Mathf.Lerp(initialVolume, volume, Mathf.Clamp01(time / setTime));
            source.volume = this.volume;
            yield return new WaitForSeconds(currentDeltaTime);
        }
        this.volume = volume;
        source.volume = this.volume;
    }

    public void Stop()
    {
        source.Stop();
    }

    public void Stop(MonoBehaviour parent, float time)
    {
        parent.StartCoroutine(StopSoundRoutine(time));
    }

    private IEnumerator StopSoundRoutine(float stopTime)
    {
        float initialVolume = source.volume;
        float time = stopTime;
        while (time > 0)
        {
            float currentDeltaTime = Time.deltaTime;
            time -= currentDeltaTime;
            yield return new WaitForSeconds(currentDeltaTime);
            volume = Mathf.Clamp(time, 0, stopTime) / stopTime * initialVolume;
            source.volume = volume;
        }
        source.Stop();
    }
}
