using UnityEngine;
[System.Serializable]
public struct GameColor
{
    public Color color;
    public int colorAmount;
    public GameColor(Color color, int colorAmount)
    {
        this.color = color;
        this.colorAmount = colorAmount;
    }
}