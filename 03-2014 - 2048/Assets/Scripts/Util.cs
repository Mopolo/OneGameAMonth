using System.Collections;
using System.Linq;
using UnityEngine;

public class Util
{
    private static float[] _x = { -1.65f, -0.55f, 0.55f, 1.65f };
    private static float[] _y = { 1.65f, 0.55f, -0.55f, -1.65f };

    public static int GetTilePositionX(float x)
    {
        for (int i = 0; i < 4; i++)
        {
            if (_x[i].Equals(x)) return i;
        }
        
        return -1;
    }

    public static float GetTilePositionX(int x)
    {
        return _x[x];
    }

    public static int GetTilePositionY(float y)
    {
        for (int i = 0; i < 4; i++)
        {
            if (_y[i].Equals(y)) return i;
        }
        
        return -1;
    }

    public static float GetTilePositionY(int y)
    {
        return _y[y];
    }

    public static void DisplayCoords(float x, float y)
    {
        Debug.Log("[" + x + ";" + y + "]");
    }
}
