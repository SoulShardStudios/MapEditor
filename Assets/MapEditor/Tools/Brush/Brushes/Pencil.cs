using UnityEngine;
using SoulShard.Utils;
using SoulShard.Math;
public class Pencil : Brush
{
    public Pencil(BrushType type) : base(type) { this.type = type; }
    protected Vector2Int? _prevMouseOffset;
    public override void Update(Vector2Int mouseOffset, Color color) => PaintUpdate(mouseOffset, color, BrushShapes.middlePlusCardinals);
    protected virtual void PaintUpdate(Vector2Int mouseOffset, Color color, Vector2Int[] brushShape)
    {
        if (Input.GetMouseButton(0))
        {
            // generates positions for a pencil stroke, checks if it can place it without using too many of one color, and then
            // places and updates the color amounts.
            Vector2Int[] pixelPoses = PaintPositions(mouseOffset, brushShape);
            _manager.GetPixelMap("Main").SetPixels(color, pixelPoses);
            _prevMouseOffset = mouseOffset;
        }
        else
            _prevMouseOffset = null;
    }
    protected Vector2Int[] PaintPositions(Vector2Int mouseOffset, Vector2Int[] brushShape)
    {
        if (_prevMouseOffset.HasValue)
            return LineRenderUtility.InterpolateLineWithShapeApplied(_prevMouseOffset.Value, mouseOffset, brushShape);
        else
            return VectorMath.TranslateVectorArray(brushShape, mouseOffset);
    }

}