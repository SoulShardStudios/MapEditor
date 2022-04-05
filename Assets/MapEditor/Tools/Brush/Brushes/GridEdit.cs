using UnityEngine;

public class GridEdit : Brush
{
    public GridEdit(BrushType type) : base(type) { this.type = type; }

    public override void Update(Vector2Int mouseOffset, Color color)
    {
        if (Input.GetMouseButtonDown(0))
            ChunkEdit(mouseOffset);
    }

    void ChunkEdit(Vector2Int mouseOffset)
    {

    }
}
