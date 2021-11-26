using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_VideoStop : ObjectInteraction
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
        videoController.VideoStop();
    }
}
