using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Warning : MonoBehaviour
{
    [SerializeField]
    Text warningText;
    // Start is called before the first frame update
    void Start()
    {
        //warningText = gameObject.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnOnWarningCV()
    {
        gameObject.SetActive(true);
    }
    public void TurnOffWarningCV()
    {
        gameObject.SetActive(false);
    }

    public void PopWarningMsg(string message)
    {
        warningText.text = message;
        TurnOnWarningCV();
    }
}
