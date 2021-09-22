using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CharacterMoveController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Transform characterBody;
    [SerializeField]
    private Transform cameraArm;
    
    public float moveSpeed = 5.0f;
    public PhotonView PV;

    Animator animator;
    string animationState = "Run";
    Vector3 moveDir;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        animator = GetComponent<Animator>();
        gameObject.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        LookAround();
        Move();
    }

    private void LookAround()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 camAngle = cameraArm.rotation.eulerAngles;
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

        cameraArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
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
                Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
                Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
                moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

                //characterBody.forward = moveDir;
                //PV.RPC("changeForward", RpcTarget.All, moveDir);
                this.characterBody.transform.Rotate(moveDir.x, moveDir.y, moveDir.z);
                this.transform.position += moveDir.normalized * moveSpeed * Time.deltaTime;
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
