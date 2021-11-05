using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PDFCameraOff : MonoBehaviour
{
    public GameObject camera1;

    private void Update()
    {
        if (camera1.activeSelf)
        {

            if (Input.GetButtonDown("Cancel"))
            {
                camera1.SetActive(false);
            }
        }
    }

    public void OnCursor()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
}
