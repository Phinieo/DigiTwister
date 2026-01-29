using UnityEngine;
using System;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource narrationSource;

    [SerializeField] private AudioSource effectSource;

    [SerializeField] private AudioSource musicSource;

    public bool IsNarrationPlaying => narrationSource.isPlaying;
    public bool IsEffectPlaying => effectSource.isPlaying;
    public bool IsMusicPlaying => musicSource.isPlaying;

    public AudioClip CurrentNarrationClip => narrationSource.clip;
    public AudioClip CurrentEffectClip => effectSource.clip;
    public AudioClip CurrentMusicClip => musicSource.clip;

    // Events
    public event Action<AudioClip> OnNarrationClipStarted;
    public event Action<AudioClip> OnNarrationClipStopped;

    public event Action<AudioClip> OnEffectClipStarted;
    public event Action<AudioClip> OnEffectClipStopped;

    public event Action<AudioClip> OnMusicClipStarted;
    public event Action<AudioClip> OnMusicClipStopped;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        // Detect natural clip end
        if (narrationSource.clip != null &&
            !narrationSource.isPlaying &&
            narrationSource.time > 0f)
        {
            AudioClip finishedClip = narrationSource.clip;
            narrationSource.clip = null;
            narrationSource.time = 0f;

            OnNarrationClipStopped?.Invoke(finishedClip);
        }

        // Detect natural clip end
        if (effectSource.clip != null &&
            !effectSource.isPlaying &&
            effectSource.time > 0f)
        {
            AudioClip finishedClip = effectSource.clip;
            effectSource.clip = null;
            effectSource.time = 0f;

            OnEffectClipStopped?.Invoke(finishedClip);
        }

        // Detect natural clip end
        if (musicSource.clip != null &&
            !musicSource.isPlaying &&
            musicSource.time > 0f)
        {
            AudioClip finishedClip = musicSource.clip;
            musicSource.clip = null;
            musicSource.time = 0f;

            OnMusicClipStopped?.Invoke(finishedClip);
        }
    }

    public void PlayNarrationClip(AudioClip clip)
    {
        if (clip == null)
            return;

        StopNarrationClip();

        narrationSource.clip = clip;
        narrationSource.Play();

        OnNarrationClipStarted?.Invoke(clip);
    }

    public void PlayEffectClip(AudioClip clip)
    {
        if (clip == null)
            return;

        StopEffectClip();

        effectSource.clip = clip;
        effectSource.Play();

        OnEffectClipStarted?.Invoke(clip);
    }

    public void PlayMusicClip(AudioClip clip)
    {
        if (clip == null)
            return;

        StopMusicClip();

        musicSource.clip = clip;
        musicSource.Play();

        OnMusicClipStarted?.Invoke(clip);
    }

    public void StopNarrationClip()
    {
        if (!narrationSource.isPlaying)
            return;

        AudioClip stoppedClip = narrationSource.clip;

        narrationSource.Stop();
        narrationSource.clip = null;
        narrationSource.time = 0f;

        OnNarrationClipStopped?.Invoke(stoppedClip);
    }

    public void StopEffectClip()
    {
        if (!effectSource.isPlaying)
            return;

        AudioClip stoppedClip = effectSource.clip;

        effectSource.Stop();
        effectSource.clip = null;
        effectSource.time = 0f;

        OnEffectClipStopped?.Invoke(stoppedClip);
    }

    public void StopMusicClip()
    {
        if (!musicSource.isPlaying)
            return;

        AudioClip stoppedClip = musicSource.clip;

        musicSource.Stop();
        musicSource.clip = null;
        musicSource.time = 0f;

        OnMusicClipStopped?.Invoke(stoppedClip);
    }
}
