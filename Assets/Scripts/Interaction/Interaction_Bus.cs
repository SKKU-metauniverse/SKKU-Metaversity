using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_Bus : ObjectInteraction
{
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }
    public override void Interaction()
    {
        audioSource.Play();
    }
}
