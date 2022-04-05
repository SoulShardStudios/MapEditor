using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoulShard.PixelMaps;
public class WorldManager : MonoBehaviour
{
    PixelMap _currently_editing_map;
    [SerializeField] GameObject pixelMap;
    [SerializeField] uint universalChunkSize, startingPPU;
}
