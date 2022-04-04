using UnityEngine;
using SoulShard.PixelMaps;
public partial class MapSaveManager: MonoBehaviour
{
    #region Fields
    [SerializeField] PixelMapManager _mapManager;
    [SerializeField] string _levelName;
    [SerializeField] bool _onEnableLoadLevel;
    PixelMapSaver[] _savers { get => _mapManager.savers; }
    #endregion
    public static MapSaveManager S { get; private set;}
    #region Unity Messages
    void OnEnable()
    {
        S = this;
        _mapManager.OnInitCompleted += Init;
    }
    void OnDisable()
    {
        _mapManager.OnInitCompleted -= Init;
    }
    #endregion
    #region Init
    void Init()
    {
        InitMaps();
        InitMapPaths();
        if (_onEnableLoadLevel)
            Load();
    }
    void InitMaps()
    {
        foreach (PixelMapSaver s in _savers)
            if (s != null)
                s.Init();
    }
    void InitMapPaths(string levelPath = "")
    {
        string levelSaveLocation = levelPath == "" ? $"{Constants.LevelSaveFilePath}/{_levelName}" : levelPath;
        foreach (PixelMapSaver s in _savers)
            if (s != null)
                s.saveLocation = $"{levelSaveLocation}/{s.gameObject.name}";
    }
    #endregion
    #region Load/Save
    public void LoadLevel(string levelPath)
    {
        InitMapPaths(levelPath);
        Load();
    }
    public void Save()
    {
        foreach (PixelMapSaver s in _savers)
            if (s != null)
                s.SaveData();
    }
    public void Load()
    {
        foreach (PixelMapSaver s in _savers)
            if (s != null)
                s.LoadData();
    }
    #endregion
}