using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public AudioSource audioSource;
    private bool isMuted = false;

    public void videoPlay() 
    {
        videoPlayer.Play();
        audioSource.Play();
    }

    public void videoPause()
    {
        
        videoPlayer.Pause();
        audioSource.Pause();
    }

    public void videoStop()
    {
        videoPlayer.Stop();
        audioSource.Stop();
    }

    public void videoVolume(float vol)
    {
        audioSource.volume = vol;

    }

    public void SetVideoMute()
    {
        if (!isMuted)
        {
            videoPlayer.SetDirectAudioMute(0, true);
        }
        else 
        {
            videoPlayer.SetDirectAudioMute(0, false);
        }
    }

}
