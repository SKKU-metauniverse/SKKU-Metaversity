using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_Bus : ObjectInteraction
{
    private AudioSource audioSource;
    private void Start()
    {
        outline = transform.GetComponent<Outline>();
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }
    public override void Interaction()
    {
        audioSource.Play();
    }
}
