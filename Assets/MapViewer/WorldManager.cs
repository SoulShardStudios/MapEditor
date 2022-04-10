using UnityEngine;
using SoulShard.PixelMaps;
using System.Collections.Generic;
public class WorldManager : MonoBehaviour
{
    public static WorldManager Instance { get; private set; }

    Dictionary<string,List<PixelMap>> maps;
    [SerializeField] GameObject pixelMap;
    [SerializeField] uint universalChunkSize, startingPPU;
    
}
