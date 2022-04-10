using UnityEngine;
using SoulShard.PixelMaps;
using System.Collections.Generic;
public partial class PixelMapManager
{
    public GameObject[] pixelMapReferences;
    [HideInInspector] public PixelMap[] maps;
    [HideInInspector] public PixelMapSaver[] savers;
    [HideInInspector] public bool initialized = false;
    public void InitRefs()
    {
        initialized = true;
        List<PixelMap> maps = new List<PixelMap>(0);
        for (int i = 0; i < pixelMapReferences.Length; i++)
            maps.Add(pixelMapReferences[i].GetComponent<PixelMap>());
        this.maps = maps.ToArray();
        List<PixelMapSaver> savers = new List<PixelMapSaver>(0);
        for (int i = 0; i < pixelMapReferences.Length; i++)
            savers.Add(pixelMapReferences[i].GetComponent<PixelMapSaver>());
        this.savers = savers.ToArray();
    }
}