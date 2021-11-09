using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractingObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
        Debug.DrawRay(transform.position, transform.forward * maxDistance * 0.95f, Color.blue, 0.3f);
        Debug.DrawRay(transform.position, transform.forward * maxDistance * 1.05f, Color.blue, 0.3f);
    }
}
