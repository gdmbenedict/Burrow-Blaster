using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioRandomizer : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private AudioSource audioSource; 
    [SerializeField] private float pitchVariance;
    [SerializeField] private bool looping;
    [SerializeField] private bool playOnAwake;
    [SerializeField] private bool gameplayAudio;

    private int trackTime = 0;
    private int lastTrackTime;
    private float frequency;
    private SFXManager sfxManager;

    // Start is called before the first frame update
    void Awake()
    {
        //add to list of gameplay audio if audio source belongs to gameplay
        if (gameplayAudio)
        {
            sfxManager = FindObjectOfType<SFXManager>();
            sfxManager.gameplayAudioSources.Add(audioSource);
        } 

        //grab frequency for song for calculations
        if (looping)
        {
            frequency = audioClip.frequency;
        }

        //start playing
        if (playOnAwake && audioClip != null)
        {
            Play(audioClip);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //loop finished, edit pitch
        if (trackTime == 0 && trackTime != trackTime)
        {
            RandomizePitch();
        }

        if (looping && audioSource.isPlaying)
        {
            //calculate track time for looping
            int trackTime = (int)(audioSource.timeSamples / (frequency));

            if (trackTime != lastTrackTime)
            {
                trackTime = lastTrackTime;
            }
        }
    }

    //Function handling playing audio from the source
    public void Play(AudioClip audioClip)
    {
        RandomizePitch();

        if (looping)
        {
            audioSource.clip = audioClip;
            audioSource.loop = true;
            audioSource.Play();
        }
        else
        {
            audioSource.PlayOneShot(audioClip);
        }
    }

    //Function to return if the audio source is playing
    public bool IsPlaying()
    {
        return audioSource.isPlaying;
    }

    //Function that handles randomizing the pitch of the audio source;
    private void RandomizePitch()
    {
        audioSource.pitch = 1 + Random.Range(-pitchVariance, pitchVariance);
    }

    //remove self from sound manager if unloaded
    private void OnDestroy()
    {
        if (gameplayAudio)
        {
            sfxManager.gameplayAudioSources.Remove(audioSource);
        }
    }
}
