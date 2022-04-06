using UnityEngine;
using SoulShard.FileSystem;
using System.Collections.Generic;
using Newtonsoft.Json;
public class SelectWorkspace : MonoBehaviour
{
    public GameObject mapSelection, mapSelectionParent;
    List<string> currentWorkspaces = new List<string>(0);
    const string _workspacesFolder = "<persistentdata>/Workspaces";
    private void OnEnable()
    {
        currentWorkspaces.AddRange(GetWorkspaces()); 
        renderWorkspaces(currentWorkspaces.ToArray());
    }

    public void NewWorkspace(string name)
    {
        if (name == null)
            return;
        if (currentWorkspaces.Contains(name))
            return;

        InitializeWorkspace($"{_workspacesFolder}/{name}", name);
        currentWorkspaces.Add(name);
        renderWorkspaces(currentWorkspaces.ToArray());
    }

    public void InitializeWorkspace(string path, string name)
    {
        if (path == null)
            return;
        DirectoryUtility.CreateDir(path);
        FileUtility.Make(
            $"{path}/config.json",
            JsonConvert.SerializeObject(new MapConfig(name))
            );
    }

    string[] GetWorkspaces()
    {
        if (!DirectoryUtility.Exists(_workspacesFolder))
            DirectoryUtility.CreateDir(_workspacesFolder);

        string[] directories = DirectoryUtility.GetAllDirectories(_workspacesFolder);
        string[] @return = new string[directories.Length];
        for (int i= 0; i < directories.Length; i++)
            @return[i] = PathUtility.GetPartOfPath(directories[i], 0, false, '\\');
        return @return;
    }

    void deleteWorkspace(string name)
    {

    }

    void renderWorkspaces(string[] names)
    {
        MapSelection[] currentChildren = mapSelectionParent.GetComponentsInChildren<MapSelection>();
        foreach (string name in names)
        {
            bool contained = false;
            foreach (MapSelection child in currentChildren)
                if (child.name == name)
                    contained = true;

            if (!contained)
            {
                GameObject g = Instantiate(mapSelection, mapSelectionParent.transform);
                MapSelection selection = g.GetComponent<MapSelection>();
                string path = PathUtility.ParsePath($"{_workspacesFolder}/{name}/config.json");
                string text = AssetUtility.LoadText(path);
                MapConfig config = JsonConvert.DeserializeObject<MapConfig>(text);
                selection.Init(config.name, name, config.description);
            }
        }
    }
}
