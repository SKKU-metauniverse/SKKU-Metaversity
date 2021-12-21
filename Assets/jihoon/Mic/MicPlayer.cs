using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Voice.Unity;
using Photon.Voice.PUN;

public class MicPlayer : MonoBehaviourPun
{
    /*
    public AudioSource audSourse;

    public AudioClip aud;
    int sampleRate = 44100;
    private float[] samples;
    public float rmsValue;
    public float modulate;

    public int resultValue; //결괏값 ( 0 ~ 100 )
    public int cutValue; // 최저 볼륨
    */
    public KeyCode voicBtn = KeyCode.P;
    private Recorder voiceRecorder;
    private PhotonView view;

    private void Start()
    {
        voiceRecorder = GetComponent<Recorder>();
        view = photonView;
        voiceRecorder.TransmitEnabled = false;

        /*
        samples = new float[sampleRate];
        aud = Microphone.Start(Microphone.devices[0].ToString(), true, 1, sampleRate);
        
        audSourse.clip = aud;
        audSourse.Play();
        */
    }

    private void Update()
    {
        /*
        aud.GetData(samples, 0);

        float sum = 0;
        for (int i = 0; i < samples.Length; i++)
        {
            sum += samples[i] * samples[i];
        }
        rmsValue = Mathf.Sqrt(sum / samples.Length);
        rmsValue *= modulate;
        rmsValue = Mathf.Clamp(rmsValue, 0, 100);
        resultValue = Mathf.RoundToInt(rmsValue);

        if ( resultValue < cutValue )
        {
            resultValue = 0;
            audSourse.mute = true;
        }
        else
        {
            audSourse.mute = false;
        }

        */

        if (Input.GetKeyDown(voicBtn))
        {
            if (view.IsMine)
            {
                voiceRecorder.TransmitEnabled = true;
            }
        } else if (Input.GetKeyUp(voicBtn))
        {
            if (view.IsMine)
            {
                voiceRecorder.TransmitEnabled = false;
            }
        }
        
    }

}
