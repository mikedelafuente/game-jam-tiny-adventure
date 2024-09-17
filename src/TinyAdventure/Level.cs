using System.Numerics;
using Raylib_cs;

namespace TinyAdventure;

public class Level
{
    public void Init()
    {

    }
    public void Draw(Camera2D camera)
    {

    }

    public void Update(GameState gameState)
    {
    }

    public void Cleanup(){}
}


public static class Helper
{
    public static int NearestIncrement(int value, int increment)
    {
        int remainder = value % increment;
        if (remainder == 0)
        {
            return value;
        }

        return (int)Math.Round((float)value / increment) * increment;
    }
    public static Vector2 NearestIncrementPoint(Vector2 value, int increment)
    {
        int x = NearestIncrement((int)value.X, increment);
        int y = NearestIncrement((int)value.Y, increment);

        return new Vector2(x, y);
    }

}
