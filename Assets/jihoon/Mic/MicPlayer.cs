using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicPlayer : MonoBehaviour
{
    public AudioSource audSourse;

    public AudioClip aud;
    int sampleRate = 44100;
    private float[] samples;
    public float rmsValue;
    public float modulate;

    public int resultValue; //결괏값 ( 0 ~ 100 )
    public int cutValue; // 최저 볼륨

    private void Start()
    {

        samples = new float[sampleRate];
        aud = Microphone.Start(Microphone.devices[0].ToString(), true, 1, sampleRate);
        
        audSourse.clip = aud;
        audSourse.Play();

    }

    private void Update()
    {
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

        
        
    }

}
