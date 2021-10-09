using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public AudioSource[] soundAudio;

    public void PlayCoinSound()
    {
        soundAudio[0].Play();
    }

    public void PlayFootstepsSound()
    {
        soundAudio[1].Play();
    }

    public void PlayJumpSound()
    {
        soundAudio[2].Play();
    }

    public void PlayLandingSound()
    {
        soundAudio[3].Play();
    }

    public void PlayBushSound()
    {
        soundAudio[4].Play();
    }

    public void PlaySlotmachineLeverSound()
    {
        soundAudio[5].Play();
    }

    public void PlayThrowSound()
    {
        soundAudio[6].Play();
    }

    public void PlayWinSound()
    {
        soundAudio[7].Play();
    }

    public void PlayLoseSound()
    {
        soundAudio[8].Play();
    }

    public void PlayMenuSound()
    {
        soundAudio[9].Play();
    }
}
