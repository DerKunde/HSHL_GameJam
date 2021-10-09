using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSettings : MonoBehaviour
{
    private static readonly string MusicPref = "MusicPref";
    private static readonly string SoundPref = "SoundPref";
    private float musicFloat, soundFloat;
    public AudioSource musicAudio;
    public AudioSource menuAudio;
    public AudioSource[] soundAudio;

    //Tutorial: https://www.youtube.com/watch?v=9ROolmPSC70

    private void Awake() {
        ContinueAudioSettings();
    }

    private void ContinueAudioSettings(){
        musicFloat = PlayerPrefs.GetFloat(MusicPref);
        soundFloat = PlayerPrefs.GetFloat(SoundPref);

        musicAudio.volume = musicFloat;
        menuAudio.volume = musicFloat;
        

        for (int i = 0; i< soundAudio.Length; i++){
            soundAudio[i].volume = soundFloat;
        }
    }
}
