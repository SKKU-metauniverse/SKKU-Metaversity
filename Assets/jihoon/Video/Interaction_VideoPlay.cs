using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Interaction_VideoPlay : ObjectInteraction
{
    [SerializeField]
    private VideoPlayer vp;
    [SerializeField]
    private AudioSource audioSource;

    private void Start()
    {
        outline = transform.GetComponent<Outline>();
        
        audioSource = gameObject.GetComponentInParent<AudioSource>();
        vp = gameObject.GetComponentInParent<VideoPlayer>();
    }

    public override void Interaction()
    {
        vp.Play();
        audioSource.Play();
    }
}
