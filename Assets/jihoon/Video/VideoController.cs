using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public AudioSource audioSource;
    private bool isMuted = false;

    public void VideoPlay() 
    {
        videoPlayer.Play();
        audioSource.Play();
    }

    public void VideoPause()
    {
        
        videoPlayer.Pause();
        audioSource.Pause();
    }

    public void VideoStop()
    {
        videoPlayer.Stop();
        audioSource.Stop();
    }

    public void VideoVolume(float vol)
    {
        audioSource.volume = vol;

    }

    public void SetVideoMute()
    {
        if (!isMuted)
        {
            audioSource.mute = true;
            isMuted = true;
        }
        else 
        {
            audioSource.mute = false;
            isMuted = false;
        }
    }

}
