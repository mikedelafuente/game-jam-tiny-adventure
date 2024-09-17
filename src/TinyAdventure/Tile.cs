using System.Numerics;
using Raylib_cs;

namespace TinyAdventure;


public class Tile : Entity
{
    private Tile()
    {
    }

    public static Tile StandardPlatform(Vector2 position)
    {
        var newTile = new Tile();
        // TODO: REvert to width being from a texture
        newTile.HitBox = new Rectangle(position, 96, 6); // Thin landing
        newTile.Position = position;
        //newTile.SetCurrentActivity(Action.Static, true);

        return newTile;
    }
}
