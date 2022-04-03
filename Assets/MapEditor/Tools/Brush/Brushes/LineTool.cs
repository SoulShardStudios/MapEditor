using UnityEngine;
using SoulShard.Utils;
public class LineTool : Brush
{
    public LineTool(BrushType type) : base(type) { this.type = type; }
    Vector2Int? _startingMouseOffset;
    Vector2Int? _previousMouseOffset;
    Vector2Int _mouseOffset;
    bool _lineIsHeld;
    public override void Update(Vector2Int mouseOffset, Color color) {
        _mouseOffset = mouseOffset;
        if (_lineIsHeld && Input.GetMouseButtonDown(1))
            EndLineTrace(false, color);
        if (Input.GetMouseButtonDown(0))
        {
            _lineIsHeld = !_lineIsHeld;
            if (_lineIsHeld)
                _startingMouseOffset = _mouseOffset;
            else
                EndLineTrace(true, color);
        }
        if (_lineIsHeld)
            if(_previousMouseOffset.HasValue ? _previousMouseOffset != _mouseOffset : true)
                UpdateTilemapWithLineTool("Overlay", false, color);
        _previousMouseOffset = _mouseOffset;
    }
    private void EndLineTrace(bool addToPixelMap, Color color)
    {
        _lineIsHeld = false;
        _manager.GetPixelMap("Overlay").ClearAll(false);
        if (addToPixelMap)
            UpdateTilemapWithLineTool("Main", true, color);
    }
    private void UpdateTilemapWithLineTool(string map, bool isPlaced, Color color)
    {
        // interpolates a line and copies it to an array
        if (!isPlaced)
            _manager.GetPixelMap(map).ClearAll(true);
        Vector2Int[] positions = LineRenderUtility.InterpolateLineWithShapeApplied(_startingMouseOffset.Value, _mouseOffset, BrushShapes.middlePlusCardinals);
        _manager.GetPixelMap(map).SetPixels(color, positions);
    }
}