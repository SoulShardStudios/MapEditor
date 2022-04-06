using UnityEngine;
using System;

[Serializable]
public struct MapConfig
{
    public string name;
    public string description;
    public uint editorVersion;
    public uint chunkSize;
    public uint basePixelsPerUnit;
    public MapLayer[] layers;
    public FeatureType[] legend;
    public MapConfig(string name)
    {
        basePixelsPerUnit = 100;
        chunkSize = 100;
        description = "";
        editorVersion = Constants.VersionNumber;
        layers = new MapLayer[0] { };
        legend = new FeatureType[0] { };
        this.name = name;
    }
}

[Serializable]
public struct MapLayer
{
    public string name;
    public string description;
    public uint depth;
    public Marker[] markers;
}

[Serializable]
public struct FeatureType
{
    public string iconPath;
    public string type;
}

[Serializable]
public struct Marker
{
    public Vector2 position;
    public FeatureType type;
    public string title;
    public string description;
}
