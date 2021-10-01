using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum DataType { Directory = 0, File }

public class Data : MonoBehaviour
{
    [SerializeField]
    private Sprite[] spriteIcons;

    private Image imageIcon;
    private TextMeshProUGUI textDataName;

    private DataType dataType;

    private string fileName;
    public string FileName => fileName;

    private int maxFileNameLength = 25;

    public void Setup(string fileName, DataType dataType)
    {
        imageIcon = GetComponentInChildren<Image>();
        textDataName = GetComponentInChildren<TextMeshProUGUI>();

        this.fileName = fileName;
        this.dataType = dataType;

        imageIcon.sprite = spriteIcons[(int)this.dataType];

        textDataName.text = this.fileName;

        if (fileName.Length >= maxFileNameLength)
        {
            textDataName.text = fileName.Substring(0, maxFileNameLength);
            textDataName.text += "..";
        }

        SetTextColor();
    }


    private void SetTextColor()
    {
        if (dataType == DataType.Directory)
        {
            textDataName.color = Color.yellow;
        }
        else
        {
            textDataName.color = Color.white;
        }

    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
