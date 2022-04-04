using UnityEngine;
using System.Collections.Generic;
using SoulShard.Math;
using System.Threading.Tasks;
public class FillTool : Brush
{
    public FillTool(BrushType type) : base(type) { this.type = type; }

    public override void Update(Vector2Int mouseOffset, Color color)
    {
        if (Input.GetMouseButtonDown(0))
            FillArea(mouseOffset, color, 10000);
    }
    async void FillArea(Vector2Int position, Color color, int maxPixelChange)
    {
        var Main = _manager.GetPixelMap("Main");
        var Unpaintable = _manager.GetPixelMap("Unpaintable");
        // gets the starting tile, and initializes some vars
        Color startingPixel = Main.GetPixel(position);
        List<Vector2Int> updatedPositions = new List<Vector2Int>(0);
        updatedPositions.Add(position);
        HashSet<Vector2Int> positionsToCheck = new HashSet<Vector2Int>();
        int pixelsChanged = 0;

        while (true)
        {
            // if there are no positions left to update, or it goes over the fill limit, or it cant place the amount of colors it returns.
            if (updatedPositions.Count == 0)
                return;
            pixelsChanged += updatedPositions.Count;
            if (pixelsChanged > maxPixelChange)
                return;
            // otherwise take all of the positions from the last iteration and add them to the tilemap
            Vector2Int[] updatedArr = updatedPositions.ToArray();
            // then manage colors
            Main.SetPixels(color, updatedArr);
            // then checks the surrounding tiles to add to the next iteration
            foreach (Vector3Int v in updatedPositions)
                positionsToCheck.UnionWith(VectorMath.TranslateVectorArray(VectorConstants.CardinalsAndDiagonalsZeroVi(), (Vector2Int)v));
            updatedPositions.Clear();


            Vector2Int[] positionsToCheckArr = new Vector2Int[positionsToCheck.Count];
            positionsToCheck.CopyTo(positionsToCheckArr);
            Color[] MainPix = Main.GetPixels(positionsToCheckArr);
            Color[] UnpaintPix = Unpaintable.GetPixels(positionsToCheckArr);

            var res = await Task.Run(()=> {
                List<Vector2Int> updatedPoses = new List<Vector2Int>(0);
                for (int i = 0; i < positionsToCheck.Count; i++)
                {
                    if (MainPix[i] != startingPixel)
                        continue;
                    if (UnpaintPix[i] != Unpaintable.emptyPixelColor)
                        continue;
                    updatedPoses.Add(positionsToCheckArr[i]);
                }
                return updatedPoses;
            });
            updatedPositions = res;
            positionsToCheck.Clear();
        }
    }
}