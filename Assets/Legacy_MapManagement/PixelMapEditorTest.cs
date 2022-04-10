using UnityEngine;
using SoulShard.Math;
using System;
using SoulShard.PixelMaps;
using System.Collections.Generic;
using SoulShard.Utils;
using System.Linq;
[RequireComponent(typeof(PixelMapManager))]
public class PixelMapEditorTest : MonoBehaviour
{
    PixelMapManager _manager;
    Vector2Int _mouseOffset { get => (Vector2Int)VectorMath.RoundVector(Camera.main.ScreenToWorldPoint(Input.mousePosition) * Constants.UniversalPixelsPerUnit); }
    void OnEnable()
    {
        _manager = GetComponent<PixelMapManager>();
        _manager.Init();
        foreach (Brush b in Brushes.brushes)
            b.Init(_manager);
        //InitMaps();
    }
    void Update()
    {
        //foreach (Brush b in Brushes.brushes)
        //    if (b.type == BrushManager.S.brushType)
        //        b.Update(_mouseOffset, ColorManager.S.currentColor);
    }

}