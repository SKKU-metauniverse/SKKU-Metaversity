using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CharacterMoveController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject characterBody;
    [SerializeField]
    private GameObject cameraArm;

    public float moveSpeed = 5.0f;
    public PhotonView PV;
    public Camera Cmine;

    Animator animator;
    string animationState = "Run";
    Vector3 moveDir;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        animator = GetComponent<Animator>();
        gameObject.SetActive(true);
        if (!PV.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
        }
        Cmine.enabled = true;

        cameraArm.transform.position = new Vector3(characterBody.transform.position.x, characterBody.transform.position.y + (float)0.5, characterBody.transform.position.z - (float)0.27);

        Debug.Log(characterBody.transform.localPosition);
        Debug.Log(cameraArm.transform.localPosition);
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
    }

    private void Update()
    {
        LookAround();
        Move();
    }

    private void LookAround()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 camAngle = cameraArm.transform.rotation.eulerAngles;
        float x = camAngle.x - mouseDelta.y;

        if (x < 180f)
        {
            x = Mathf.Clamp(x, -1f, 70f);
        } else
        {
            x = Mathf.Clamp(x, 335f, 361f);
        }

        // mouseDelta Up(+) and Down(-) : y, Left(-) and Right(+) : x
        // camAngle Up(-) and Down(+) : x, Left(-) and Right(+) : y, Front and Back : z
        // Debug.Log(mouseDelta);

        cameraArm.transform.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
    }

    private void Move()
    {
        if (PV.IsMine)
        {
            Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            bool isMove = moveInput.magnitude != 0;
            animator.SetBool(animationState, isMove);

            if (isMove)
            {
                Vector3 lookForward = new Vector3(cameraArm.transform.forward.x, 0f, cameraArm.transform.forward.z).normalized;
                Vector3 lookRight = new Vector3(cameraArm.transform.right.x, 0f, cameraArm.transform.right.z).normalized;
                moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

                //Debug.Log(GetAngle(characterBody.forward, moveDir));
                //Debug.DrawRay(characterBody.position, characterBody.forward, Color.red);
                //Debug.DrawRay(characterBody.position, moveDir, Color.blue);
                
                characterBody.transform.forward = moveDir;
                //pv.rpc("changeforward", rpctarget.all, movedir);
                //characterBody.transform.Rotate(new Vector3(0, GetAngle(characterBody.forward, moveDir), 0));
                

                this.transform.position += moveDir.normalized * moveSpeed * Time.deltaTime;
                //cameraArm.transform.position = new Vector3(characterBody.transform.position.x, characterBody.transform.position.y + (float)0.5, characterBody.transform.position.z - (float)0.27);
                cameraArm.transform.position = new Vector3(characterBody.transform.position.x, characterBody.transform.position.y + GameObject.Find("Spawn").transform.position.y + (float)0.5, characterBody.transform.position.z - (float)0.27);

                Debug.Log(characterBody.transform.localPosition);
                Debug.Log(cameraArm.transform.localPosition);
            }
        }
        
    }


    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    if (stream.IsWriting)
    //    {
    //        stream.SendNext(moveDir);
    //    }
    //    else
    //    {
    //        this.moveDir = (Vector3)stream.ReceiveNext();
    //    }
    //}

    //[PunRPC]
    //void changeForward(Vector3 dir)
    //{
    //    characterBody.forward = dir;
    //}
}
