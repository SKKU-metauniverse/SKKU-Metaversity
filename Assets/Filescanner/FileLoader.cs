using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;


public class FileLoader : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject panelFileViewer;

    [SerializeField]
    private TextMeshProUGUI textFileName;
    [SerializeField]
    private TextMeshProUGUI textFileSize;
    [SerializeField]
    private TextMeshProUGUI textCreationTime;
    [SerializeField]
    private TextMeshProUGUI textLastWriteTime;
    [SerializeField]
    private TextMeshProUGUI textDirectory;
    [SerializeField]
    private TextMeshProUGUI textFullName;

    private FileInfo fileInfo;

    public void OnLoad (FileInfo file)
    {
        panelFileViewer.SetActive(true);

        fileInfo = file;

        textFileName.text = $"파일 이름 : {fileInfo.Name}";
        textFileSize.text = $"파일 크기 : {fileInfo.Length} Bytes";
        textCreationTime.text = $"파일 생성 시간 : {fileInfo.CreationTime}";
        textLastWriteTime.text = $"파일 최종 수정 시간 : {fileInfo.LastWriteTime}";
        textDirectory.text = $"파일 경로 : {fileInfo.Directory}";
        textFullName.text = $"전체 경로 : {fileInfo.FullName}";

    }

    public void OpenFile()
    {
        Application.OpenURL("file:///" + fileInfo.FullName);

    }


    public void OffLoad()
    {

        panelFileViewer.SetActive(false);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
