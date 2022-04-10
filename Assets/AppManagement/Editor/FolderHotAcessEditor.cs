using UnityEngine;
using UnityEditor;
using SoulShard.FileSystem;
[CustomEditor(typeof(FolderHotAcess))]
public class FolderHotAcessEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("persistent data"))
            ExplorerUtility.OpenFileExplorerAtPath("<persistentdata>");
        if (GUILayout.Button("data"))
            ExplorerUtility.OpenFileExplorerAtPath("<data>");
        if (GUILayout.Button("consolelog"))
            ExplorerUtility.OpenFileExplorerAtPath("<consolelog>");
        if (GUILayout.Button("streamingassets"))
            ExplorerUtility.OpenFileExplorerAtPath("<streamingassets>");
        if (GUILayout.Button("temporarycache"))
            ExplorerUtility.OpenFileExplorerAtPath("<temporarycache>");
    }
}