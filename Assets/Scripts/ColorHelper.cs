using UnityEngine;

public static class ColorHelper
{
    public static readonly Color[] Colors = { Color.gray, Color.blue, Color.cyan, Color.green, new Color(1, 0.57f, 0), new Color(0.8f, 0, 1), Color.red, Color.white, Color.yellow };

    public static readonly string[] HexColors = { "#808080", "#0000ff", "#00ffff", "#00ff00", "#ff9100", "#cc00ff", "#ff0000", "#ffffff", "#ffeb04" };
}

// Stole it from Jason Chan's https://github.com/SimpleTalkCpp/workshop-2021-03-tetris/blob/main/Assets/Scripts/MyPiece.cs
public static class MyUtil
{
    public static int EnumCount<T>()
    {
        return System.Enum.GetValues(typeof(T)).Length;
    }
}