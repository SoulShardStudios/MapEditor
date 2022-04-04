using UnityEngine;
using UnityEditor;
using SoulShard.FileSystem;
[CustomEditor(typeof(MapSaveManager))]
public class LevelSaveManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        MapSaveManager manager = (MapSaveManager)target;
        if (GUILayout.Button("SaveCurrentMapData"))
            manager.Save();
        if (GUILayout.Button("LoadCurrentMapData"))
            manager.Load();
        if (GUILayout.Button("OpenLevelsFolder"))
            ExplorerUtility.OpenFileExplorerAtPath(Constants.LevelSaveFilePath);
    }
}