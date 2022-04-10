using UnityEngine;
using SoulShard.PixelMaps;
using System;
public partial class PixelMapManager : MonoBehaviour
{
    #region Init
    public Action OnInitCompleted;
    private void OnEnable() => Init();
    public void Init()
    {
        if (initialized)
            return; 
        initialized = true;
        InitRefs();
        ImportConstantReferences();
        OnInitCompleted?.Invoke();
    }
    #endregion
    public virtual PixelMap GetPixelMap(string name)
    {
        foreach (PixelMap m in maps)
        {
            if (m == null)
                return null;
            if (m.gameObject.name.ToLower() == name.ToLower())
                return m;
        }
        return null;
    }
    void ImportConstantReferences()
    {
        foreach (PixelMap p in maps)
            if (p != null)
                p.pixelsPerUnit = Constants.UniversalPixelsPerUnit;
    }
}