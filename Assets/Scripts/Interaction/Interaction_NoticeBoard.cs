using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_NoticeBoard : ObjectInteraction
{
    [SerializeField]
    private GameObject noticePN;

    public override void Interaction()
    {
        noticePN.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void ExitNoticePN()
    {
        noticePN.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }
}
