using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class ObjectInteraction : MonoBehaviour
{
    protected Outline outline;
    private bool isLooked;

    private void Start()
    {
        outline = transform.GetComponent<Outline>();
    }

    private void FixedUpdate()
    {
        isLooked = false;
    }

    private void LateUpdate()
    {
        if (isLooked)
        {
            
            outline.enabled = true;
        }
        else
        {
            outline.enabled = false;
        }
    }

    public void Highlight()
    {
        isLooked = true;
    }

    abstract public void Interaction();
}
