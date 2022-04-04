using UnityEngine;
public static class Constants
{
    public static readonly int UniversalPixelsPerUnit = 100;
    public static readonly float UniversalColliderZSize = 0.25f;
    public static readonly string VersionName = "Alpha 0.0.0";
    public static readonly int VersionNumber = 0;
    public static readonly Vector2Int[] ScreenResolutions = { new Vector2Int(640, 360), new Vector2Int(1280, 720), new Vector2Int(1920, 1080) };


    #region SerializedConstants
    public static string ExternalAssetFilePath = "<data>/../ExternalAssets";
    public static string LevelSaveFilePath = "<data>/../ExternalAssets/LevelSaves";
    #endregion
}