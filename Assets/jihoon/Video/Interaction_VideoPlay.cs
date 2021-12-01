using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Interaction_VideoPlay : ObjectInteraction
{
    [SerializeField]
    private VideoController videoController;

    private void Start()
    {
        outline = transform.GetComponent<Outline>();
        
        videoController = gameObject.GetComponentInParent<VideoController>();
    }

    public override void Interaction()
    {
        videoController.VideoPlay();
    }
}
