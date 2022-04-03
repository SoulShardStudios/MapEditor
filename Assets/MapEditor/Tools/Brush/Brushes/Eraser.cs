using UnityEngine;
public class Eraser : Pencil
{
    public Eraser(BrushType type) : base(type) { this.type = type; }
    public override void Update(Vector2Int mouseOffset, Color color) => PaintUpdate(mouseOffset, _manager.GetPixelMap("Main").emptyPixelColor, BrushShapes._MPC2);
    protected override void PaintUpdate(Vector2Int mouseOffset, Color color, Vector2Int[] brushShape)
    {
        if (Input.GetMouseButton(0))
        {
            // same as pencil, but doesn't check for if it can place, clear is always placeable
            Vector2Int[] pixelPositions = PaintPositions(mouseOffset, brushShape);
            _manager.GetPixelMap("Main").SetPixels(color, pixelPositions);
            _prevMouseOffset = mouseOffset;
        }
        else
            _prevMouseOffset = null;
    }
}