using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public enum MixerGroup
    {
        Master,
        Music,
        SFX
    }

    [Header("Audio Mixers")]
    [SerializeField] private AudioMixer masterMixer;

    [Header("UI Elements")]
    [SerializeField] private Slider[] masterSliders;
    [SerializeField] private Slider[] musicSliders;
    [SerializeField] private Slider[] sfxSliders;


    // Start is called before the first frame update
    void Start()
    {
        //setting initial volumes
        masterMixer.SetFloat("MasterVolume", ConvertToDB(1));
        masterMixer.SetFloat("MusicVolume", ConvertToDB(1));
        masterMixer.SetFloat("SFXVolume", ConvertToDB(1));

        //setting master sliders to 1
        for (int i =0; i<masterSliders.Length; i++)
        {
            masterSliders[i].value = 0.5f;
        }

        //setting music sliders to 1
        for (int i = 0; i < musicSliders.Length; i++)
        {
            musicSliders[i].value = 0.5f;
        }

        //setting sfx sliders to 1
        for (int i = 0; i < sfxSliders.Length; i++)
        {
            sfxSliders[i].value = 0.5f;
        }
    }

    //function that sets volume of mixers
    public void SetVolume(MixerGroup target, float volume)
    {
        switch (target)
        {
            case MixerGroup.Master:
                masterMixer.SetFloat("MasterVolume", ConvertToDB(volume));
                break;

            case MixerGroup.Music:
                masterMixer.SetFloat("MusicVolume", ConvertToDB(volume));
                break;

            case MixerGroup.SFX:
                masterMixer.SetFloat("SFXVolume", ConvertToDB(volume));
                break;
        }
    }

    public void SetMasterVolume(float sliderValue)
    {
        SetVolume(MixerGroup.Master, sliderValue);
    }

    public void SetSFXVolume(float sliderValue)
    {
        SetVolume(MixerGroup.SFX, sliderValue);
    }

    public void SetMusicVolume(float sliderValue)
    {
        SetVolume(MixerGroup.Music, sliderValue);
    }

    //utility function for converting linear float to DB
    private float ConvertToDB(float value)
    {
        return Mathf.Log10(value) * 20f;
    }

}
