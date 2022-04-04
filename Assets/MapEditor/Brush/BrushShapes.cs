using UnityEngine;
public static class BrushShapes
{
    public static readonly Vector2Int[] middlePlusCardinals = new Vector2Int[] { Vector2Int.zero, Vector2Int.up, Vector2Int.down, Vector2Int.right, Vector2Int.left };
    public static readonly Vector2Int[] _MPC2 = new Vector2Int[] { Vector2Int.zero, Vector2Int.up, Vector2Int.down, Vector2Int.right, Vector2Int.left,
    new Vector2Int(-1,-1), new Vector2Int(-1,1), new Vector2Int(1,-1), new Vector2Int(1,1), new Vector2Int(2,0), new Vector2Int(-2,0), new Vector2Int(0,-2), new Vector2Int(0,2)
    };
}