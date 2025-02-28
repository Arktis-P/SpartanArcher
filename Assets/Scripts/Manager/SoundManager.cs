using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField][Range(0f, 1f)] private float soundEffectVolume;
    [SerializeField][Range(0f, 1f)] private float soundEffectPitchVariance;
    [SerializeField][Range(0f, 1f)] private float musicVolume;

    private AudioSource musicAudioSource;
    public AudioClip musicClip;

    public SoundSource soundSourcePrefab;

    public RectTransform BGMSlider;
    public RectTransform SFXSlider;

    private void Awake()
    {
        instance = this;
        musicAudioSource = GetComponent<AudioSource>();
        musicAudioSource.volume = musicVolume;
        musicAudioSource.loop = true;

        // initialize SFX and BGM slider
        if (BGMSlider != null) UpdateBGMSlider();
        if (SFXSlider != null) UpdateSFXSlider();
    }

    private void Start()
    {
        ChangeBackGroundMusic(musicClip);
    }

    public void ChangeBackGroundMusic(AudioClip clip)
    {
        musicAudioSource.Stop();
        musicAudioSource.clip = clip;
        musicAudioSource.Play();
    }

    public static void PlayClip(AudioClip clip)
    {
        SoundSource obj = Instantiate(instance.soundSourcePrefab);
        SoundSource soundSource = obj.GetComponent<SoundSource>();
        soundSource.Play(clip, instance.soundEffectVolume, instance.soundEffectPitchVariance);
    }

    // increase and decrease volumes
    public void IncreaseBGMVolumne()
    {
        musicVolume = Mathf.Clamp01(musicVolume + 0.1f);
        musicAudioSource.volume = musicVolume;
        UpdateBGMSlider();
    }
    public void DecreaseBGMVolumne()
    {
        musicVolume = Mathf.Clamp01(musicVolume - 0.1f);
        musicAudioSource.volume = musicVolume;
        UpdateBGMSlider();
    }

    public void IncreaseSFXVolume()
    {
        soundEffectVolume = Mathf.Clamp01(soundEffectVolume + 0.1f);
        UpdateSFXSlider();
    }
    public void DecreaseSFXVolume()
    {
        soundEffectVolume = Mathf.Clamp01(soundEffectVolume - 0.1f);
        UpdateSFXSlider();
    }

    // control SFX & BGM slider
    public void UpdateBGMSlider()
    {
        if (BGMSlider != null) BGMSlider.localScale = new Vector3(musicVolume, 1f, 1f);
    }
    public void UpdateSFXSlider()
    {
        if (SFXSlider != null) SFXSlider.localScale = new Vector3(soundEffectVolume, 1f, 1f);
    }
}
