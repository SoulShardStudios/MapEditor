using UnityEngine;
using SoulShard.FileSystem;
using System.Collections.Generic;
using SoulShard.Utils;
using System.IO;
public class SelectWorkspace : MonoBehaviour
{
    List<string> currentWorkspaces = new List<string>(0);
    const string _workspacesFolder = "<persistentdata>/Workspaces";
    private void OnEnable()
    {
        currentWorkspaces.AddRange(GetWorkspaces());
        DebugUtility.LogCollection(currentWorkspaces);
    }

    public void NewWorkspace(string Name)
    {
        if (Name == null)
            return;
        if (currentWorkspaces.Contains(Name))
            return;

        InitializeWorkspace($"{_workspacesFolder}/{Name}");
        currentWorkspaces.Add(Name);
    }

    public void InitializeWorkspace(string path)
    {
        if (path == null)
            return;
        DirectoryUtility.CreateDir(path);

    }

    string[] GetWorkspaces()
    {
        if (!Directory.Exists(_workspacesFolder))
            DirectoryUtility.CreateDir(_workspacesFolder);

        string[] directories = DirectoryUtility.GetAllDirectories(_workspacesFolder);
        string[] @return = new string[directories.Length];
        for (int i= 0; i < directories.Length; i++)
            @return[i] = PathUtility.GetPartOfPath(directories[i], 0, false, '\\');
        return @return;
    }
}
