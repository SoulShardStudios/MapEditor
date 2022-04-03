using UnityEngine;
using System.Collections.Generic;
using SoulShard.Math;
using System.Threading.Tasks;
public class SelectTool : Brush
{
    public SelectTool(BrushType type) : base(type) { this.type = type; }
    enum state
    {
        selection_start,
        selection_end,
        selection_paste
    }
    state _state;
    Vector2Int? _prevMouseOffset;
    Color[] _cachedColors;
    Vector2Int[] _cachedPositions;
    public override void Update(Vector2Int mouseOffset, Color color) =>
        UpdateSelect(mouseOffset);
    async void UpdateSelect(Vector2Int mouseOffset)
    {
        // for some reason splitting this stuff up into multiple functions broke all of it, despite there being no significant changes to the code.
        // either way I'm going to leave it like this for now. its less readable but I'll come back and fix it later
        var Overlay = _manager.GetPixelMap("Overlay");
        var Main = _manager.GetPixelMap("Main");
        switch (_state)
        {
            case state.selection_start:
                if (!Input.GetMouseButtonDown(0))
                    break;
                // selection positions are captured
                _prevMouseOffset = mouseOffset;
                _state++;
                break;
            case state.selection_end:

                // draws the select outline
                Overlay.ClearAll(false);
                DrawSelectOutline(_prevMouseOffset.Value, mouseOffset);
                if (Input.GetMouseButtonDown(1))
                {
                    Overlay.ClearAll(true);
                    _state = 0;
                    break;
                }
                if (!Input.GetMouseButtonUp(0))
                    break;
                // get all the individual positions that were in the selection
                var res = await Task.Run(() => { 
                    return GetSelectPositions(_prevMouseOffset.Value, mouseOffset);
                });
                _cachedPositions = res;
                // get the pixels corresponding to those positions
                _cachedColors = Main.GetPixels(_cachedPositions);
                // sets the original pixels to clear
                Main.SetPixels(Main.emptyPixelColor, _cachedPositions);
                res = await Task.Run(() => {
                    return VectorMath.TranslateVectorArray(_cachedPositions, -_prevMouseOffset.Value);
                });
                // translates those positions to the origin
                _cachedPositions = res;
                // remove transparent pixels that were in the selection
                FilterCachedSelection(ref _cachedColors, ref _cachedPositions);
                // set the transparent tilemap to those pixels,
                // for a preview of what it looks like in its new position
                Overlay.ClearAll(true);
                Overlay.SetPixels(_cachedColors, _cachedPositions);
                _state++;
                break;
            case state.selection_paste:
                if (Input.GetMouseButtonDown(1))
                {
                    Overlay.gameObject.transform.position = Vector3.zero;
                    Overlay.HardReset();
                    _state = 0;
                    break;
                }
                // translate the preview to your mouse position
                Overlay.gameObject.transform.position = ((Vector2)mouseOffset) / Constants.UniversalPixelsPerUnit;
                if (!Input.GetMouseButtonDown(0))
                    break;
                // reset the transparent tilemap
                Overlay.gameObject.transform.position = Vector3.zero;
                Overlay.HardReset();
                // move the pixels to the mouse position
                Vector2Int[] translatedCache = VectorMath.TranslateVectorArray(_cachedPositions, mouseOffset);
                // paste the pixels!!!!
                Main.SetPixels(_cachedColors, translatedCache);
                _state = 0;
                break;
        }
    }

    // draws an outline for the select tool
    void DrawSelectOutline(Vector2Int start, Vector2Int end)
    {
        List<Vector2Int> outlinePositions = new List<Vector2Int>(0);
        Vector2Int diff = end - start;
        Vector2Int absDiff = VectorMath.AbsVector(diff);
        Vector2Int dir = new Vector2Int(diff.x > 0 ? 1 : -1, diff.y > 0 ? 1 : -1);
        Vector2Int current;
        outlinePositions.Add(start);
        for (int i = 0; i < 2; i++)
        {
            current = start;
            for (int x = 0; x < absDiff.x - 1; x += 2)
            {
                current.x += dir.x * 2;
                outlinePositions.Add(new Vector2Int(current.x, start.y + (diff.y * i)));
            }
            current = start;
            for (int y = 0; y < absDiff.y - 1; y += 2)
            {
                current.y += dir.y * 2;
                outlinePositions.Add(new Vector2Int(start.x + (diff.x * i), current.y));
            }
        }
        // set the tiles to the transparent tilemap.
        _manager.GetPixelMap("Overlay").SetPixels(Color.white, outlinePositions.ToArray());
    }
    // gets all positions within the selection area
    Vector2Int[] GetSelectPositions(Vector2Int start, Vector2Int end)
    {
        Vector2Int realStart = new Vector2Int(start.x < end.x ? start.x : end.x, start.y < end.y ? start.y : end.y);
        Vector2Int realEnd = new Vector2Int(start.x > end.x ? start.x : end.x, start.y > end.y ? start.y : end.y);
        Vector2Int difference = realEnd - realStart;
        Vector2Int[] toReturn = new Vector2Int[difference.x*difference.y+1];
        int i = 0;
        for (int x = realStart.x; x < difference.x + realStart.x; x++)
            for (int y = realStart.y; y < difference.y + realStart.y; y++)
            {
                i++;
                toReturn[i] = new Vector2Int(x, y);
            }
        return toReturn;
    }
    // removes transparent pixels from the selection
    void FilterCachedSelection(ref Color[] colors, ref Vector2Int[] positions)
    {
        (Color[], Vector2Int[]) res = FilterCachedSelection(colors, positions);
        colors = res.Item1;
        positions = res.Item2;
    }
    (Color[],Vector2Int[]) FilterCachedSelection(Color[] colors, Vector2Int[] positions)
    {
        List<Color> filteredColors = new List<Color>(0);
        List<Vector2Int> filteredPositions = new List<Vector2Int>(0);
        for (int i = 0; i < colors.Length; i++)
            if (colors[i] != _manager.GetPixelMap("Main").emptyPixelColor)
            {
                filteredColors.Add(colors[i]);
                filteredPositions.Add(positions[i]);
            }
        return (filteredColors.ToArray(), filteredPositions.ToArray());
    }
}