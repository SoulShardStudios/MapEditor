using UnityEngine;
public abstract partial class Brush
{
    protected PixelMapManager _manager;
    public BrushType type;
    public Brush(BrushType type) { this.type = type; }
    public virtual void Init(PixelMapManager manager) => _manager = manager;
    public virtual void Update(Vector2Int mouseOffset, Color color) { }
}