using UnityEngine;
using SoulShard.PixelMaps;
using System.Linq;
public static class UpdateColorAmounts
{
    public static void UpdateColorAmountsCallback(Vector2Int[] editedPositions, Color[] editedColors, PixelMap map)
    {
        Color[] originalColors = map.GetPixels(editedPositions);
        Color[] unique = editedColors.Union(originalColors).Distinct().ToArray();
        GameColor[] gameColors = new GameColor[unique.Length];

        for (int i = 0; i < gameColors.Length; i++)
            gameColors[i] = new GameColor(unique[i], 0);
        for (int i = 0; i < gameColors.Length; i++)
            gameColors[i].colorAmount -= editedColors.Count(c => c == unique[i]);
        for (int i = 0; i < gameColors.Length; i++)
            gameColors[i].colorAmount += originalColors.Count(c => c == unique[i]);
    }
}
