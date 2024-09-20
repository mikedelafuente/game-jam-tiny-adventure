using System.Numerics;
using Raylib_cs;

namespace TinyAdventure;

public class Level
{

    public List<Tile> Tiles { get; set; } = new List<Tile>();

    public Level()
    {
        Gravity = 0;
    }
    public void Init()
    {
        Gravity = 1000f;

        // Ideally we would load a level here, but for now let's just put in a platform
        if (Tiles.Count == 0) {
            for (var i = -10; i < 11; i++) {
                Tiles.Add(Tile.SimpleTile(new(i * 16, 16*4), "tile_set_basic1", "cavern_r0_c1"));
            }
        }

    }
    public void Draw(Camera2D camera)
    {
        foreach (var tile in Tiles) {
            RenderHelper.Draw(tile);
        }

    }

    public void Update(GameState gameState)
    {
    }

    public void Cleanup(){}
    public float Gravity { get; set; }
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
