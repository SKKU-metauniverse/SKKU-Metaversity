using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FileLoaderSystem : MonoBehaviour
{

    private FileLoader fileLoader;

    private void Awake()
    {
        fileLoader = GetComponent<FileLoader>();
    }


    public void LoadFile(FileInfo file)
    {
        OffAllPanel();

        if ( file.FullName.Contains(".pdf") || file.FullName.Contains(".xlsx") || file.FullName.Contains(".doc") ||
            file.FullName.Contains(".pptx") || file.FullName.Contains(".hwp") || file.FullName.Contains(".text") )
        {
            fileLoader.OnLoad(file);
        }

        else
        {
            fileLoader.OnLoad(file);
        }
    }

    private void OffAllPanel()
    {
        fileLoader.OffLoad();
    }

}
