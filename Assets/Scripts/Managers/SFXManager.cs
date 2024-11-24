using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public List<AudioSource> gameplayAudioSources;

    //Function that pauses all gameplay SFX sources;
    public void PauseGameplaySFX()
    {
        foreach (AudioSource audioSource in gameplayAudioSources)
        {
            audioSource.Pause();
        }
    }

    //Function that un-pauses all gameplay SFX sources
    public void UnPauseGameplaySFX()
    {
        foreach (AudioSource audioSource in gameplayAudioSources)
        {
            audioSource.UnPause();
        }
    }
}
