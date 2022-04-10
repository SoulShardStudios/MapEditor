using UnityEngine;
using SoulShard.FileSystem;
using System.Collections.Generic;
using Newtonsoft.Json;
public class MapBrowser : MonoBehaviour
{
    public GameObject mapSelection, mapSelectionParent;
    [SerializeField] DeleteMapPrompt deletePrompt;
    List<(string,GameObject)> currentMaps = new List<(string,GameObject)>(0);
    const string _mapsFolder = "<persistentdata>/Workspaces";
    private void OnEnable()
    {
        foreach (string name in GetWorkspaces())
            currentMaps.Add((name, RenderWorkspace(name)));
    }

    int GetMapIndex(string name)
    {
        int index = -1;
        for (int i = 0; i < currentMaps.Count; i++)
            if (currentMaps[i].Item1 == name)
                index = i;
        return index;
    }

    public void NewWorkspace(string name)
    {
        if (name == null)
            return;
        if (GetMapIndex(name) != -1)
            return;

        InitializeWorkspace($"{_mapsFolder}/{name}", name);
        currentMaps.Add((name, RenderWorkspace(name)));
    }

    public void DeleteWorkspace(string name)
    {
        DirectoryUtility.Delete($"{_mapsFolder}/{name}");

        for (int i = 0; i < currentMaps.Count; i++)
        {
            if (currentMaps[i].Item1 == name)
            {
                Destroy(currentMaps[i].Item2);
                currentMaps.RemoveAt(i);
            }
        }
    }

    public void InitializeWorkspace(string path, string name)
    {
        if (path == null)
            return;
        DirectoryUtility.Create(path);
        FileUtility.Make(
            $"{path}/config.json",
            JsonConvert.SerializeObject(new MapConfig(name))
            );
    }

    string[] GetWorkspaces()
    {
        if (!DirectoryUtility.Exists(_mapsFolder))
            DirectoryUtility.Create(_mapsFolder);

        string[] directories = DirectoryUtility.GetAllDirectories(_mapsFolder);
        string[] @return = new string[directories.Length];
        for (int i= 0; i < directories.Length; i++)
            @return[i] = PathUtility.GetPartOfPath(directories[i], 0, false, '\\');
        return @return;
    }

    GameObject RenderWorkspace(string name)
    {
        GameObject g = Instantiate(mapSelection, mapSelectionParent.transform);
        MapSelection selection = g.GetComponent<MapSelection>();
        string path = PathUtility.ParsePath($"{_mapsFolder}/{name}/config.json");
        string text = AssetUtility.LoadText(path);
        MapConfig config = JsonConvert.DeserializeObject<MapConfig>(text);
        selection.Init(config.name, name, config.description, deletePrompt);
        return g;
    }
}
