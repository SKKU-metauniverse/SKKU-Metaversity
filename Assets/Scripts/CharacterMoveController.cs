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
    public GameObject wow, laugh, like, angry, emojiUI;
    public GameObject emoji_H1, emoji_H2, emoji_H3, emoji_H4;
    public ParticleSystem particle;

    Animator animator;
    string animationState = "Run";
    Vector3 moveDir;
    bool canLookAround;
    Vector2 emojiCursor = new Vector2(0, 0);
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        animator = GetComponentInChildren<Animator>();
        gameObject.SetActive(true);
        if (!PV.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
        }
        Cmine.enabled = true;

        canLookAround = true; //카메라 회전 가능 상태인지
        cameraArm.transform.position = new Vector3(characterBody.transform.position.x, characterBody.transform.position.y + (float)0.5, characterBody.transform.position.z - (float)0.27);
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
    }

    private void Update()
    {
        if (canLookAround) LookAround();
        Move();
        if (Input.GetKeyDown(KeyCode.T))
        {
            Emoji();
        }
        else if (Input.GetKey(KeyCode.T))
        {
            Emojing();
        }
        else if (Input.GetKeyUp(KeyCode.T))
        {
            EmojiOut();
        }
    }

    //이모티콘 코드 시작 ************************************************************************************
    public void Emoji()
    {
        //Cursor.lockState = CursorLockMode.Confined;
        //Cursor.visible = true;
        canLookAround = false;
        emojiUI.SetActive(true);
    }

    public void Emojing()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        emojiCursor += mouseDelta;
        emojiCursor.x = Mathf.Clamp(emojiCursor.x, -5f, 5f);
        emojiCursor.y = Mathf.Clamp(emojiCursor.y, -5f, 5f);
        float radian = Mathf.Atan2(emojiCursor.y, emojiCursor.x);
        float angle = radian * 180f / Mathf.PI;

        if (angle > -45 && angle < 45)
        {
            emoji_H1.SetActive(true);
            emoji_H2.SetActive(false);
            emoji_H3.SetActive(false);
            emoji_H4.SetActive(false);
        }
        else if (angle > 45 && angle < 135)
        {
            emoji_H1.SetActive(false);
            emoji_H2.SetActive(true);
            emoji_H3.SetActive(false);
            emoji_H4.SetActive(false);
        }
        else if (angle > 135 || angle < -135)
        {
            emoji_H1.SetActive(false);
            emoji_H2.SetActive(false);
            emoji_H3.SetActive(true);
            emoji_H4.SetActive(false);
        }
        else if (angle < -45 && angle > -135)
        {
            emoji_H1.SetActive(false);
            emoji_H2.SetActive(false);
            emoji_H3.SetActive(false);
            emoji_H4.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            if (angle > -45 && angle < 45)
            {
                EmojiAngry();
            }
            else if (angle > 45 && angle < 135)
            {
                EmojiLaugh();
            }
            else if (angle > 135 || angle < -135)
            {
                EmojiLike();
            }
            else if (angle < -45 && angle > -135)
            {
                EmojiWow();
                Debug.Log("Wow");
            }
            Debug.Log("Out");
            EmojiOut();
        }
        

        


    }

    public void EmojiOut()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        canLookAround = true;
        emojiUI.SetActive(false);
    }

    public void EmojiAngry()
    {
        StartCoroutine(EmojiOn(angry));
    }
    public void EmojiWow()
    {
        StartCoroutine(EmojiOn(wow));
    }
    public void EmojiLike()
    {
        StartCoroutine(EmojiOn(like));
    }
    public void EmojiLaugh()
    {
        StartCoroutine(EmojiOn(laugh));
    }

    IEnumerator EmojiOn(GameObject emoji)
    {
        emoji.SetActive(true);
        particle.Play();
        yield return new WaitForSeconds(1f);
        emoji.SetActive(false);
        yield return 0;
    }
    //이모티콘 코드 끝 ***************************************************************************************

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
            if (Input.GetKeyDown("escape"))
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                GameObject.Find("Canvas").transform.Find("Escape Panel").gameObject.SetActive(true);
            }

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

            }
        }
        
    }
}
