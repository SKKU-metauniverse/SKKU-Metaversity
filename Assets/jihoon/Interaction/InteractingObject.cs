using System.Collections;    // Update is called once per frame

using System.Collections.Generic;
using UnityEngine;

public class InteractingObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        CheckInteraction();
    }

    //상호작용 코드 시작 ***************************************************************************************
    private void CheckInteraction()
    {
        RaycastHit hit;
        float maxDistance = 10f;

        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance))
        {
            ObjectInteraction inter = hit.transform.GetComponent<ObjectInteraction>();
            if (inter != null)
            {
                Debug.Log("seeing");
                inter.Highlight();
                if (Input.GetKeyDown(KeyCode.F))
                {
                    (inter).Interaction();
                }
            }
        }
    }
}