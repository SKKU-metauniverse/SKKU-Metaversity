using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class DirectoryController : MonoBehaviour
{
    private DirectoryInfo currentDirectory;
    private DIrectorySpawner directorySpawner;

    private void Awake()
    {
        Application.runInBackground = true;

        directorySpawner = GetComponent<DIrectorySpawner>();
        directorySpawner.Setup();

        string desktopFoleder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
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
        
    }
}
