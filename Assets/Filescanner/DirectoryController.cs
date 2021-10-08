using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class DirectoryController : MonoBehaviour
{
    private DirectoryInfo defaultDirectory;
    private DirectoryInfo currentDirectory;
    private DIrectorySpawner directorySpawner;

    private void Awake()
    {
        Application.runInBackground = true;

        directorySpawner = GetComponent<DIrectorySpawner>();
        directorySpawner.Setup(this);

        string desktopFoleder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        defaultDirectory = new DirectoryInfo(desktopFoleder);
        currentDirectory = new DirectoryInfo(desktopFoleder);
        

        UpdateDirectory(currentDirectory);
    }

    private void UpdateDirectory(DirectoryInfo directory)
    {
        currentDirectory = directory;

        directorySpawner.UpdateDirectory(currentDirectory);

    }
    // Update is called once per frame
    void Update()
    {
        if ( Input.GetKeyDown(KeyCode.Escape))
        {
            UpdateDirectory(defaultDirectory);
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            MoveToParentFolder(currentDirectory);
        }
    }


    private void MoveToParentFolder(DirectoryInfo directory)
    {
        if (directory.Parent == null) return;

        UpdateDirectory(directory.Parent);
    }

    public void UpdateInputs(string data)
    {
        if ( data.Equals("...") )
        {
            MoveToParentFolder(currentDirectory);

            return;
        }

        foreach ( DirectoryInfo directory in currentDirectory.GetDirectories() )
        {
            if ( data.Equals(directory.Name))
            {
                UpdateDirectory(directory);

                return;
            }
        }

        foreach ( FileInfo file in currentDirectory.GetFiles() )
        {
            if (data.Equals(file.Name))
            {
                Debug.Log($"선택한 파일 이름 : {file.FullName}");
            }
        }
    }
}
