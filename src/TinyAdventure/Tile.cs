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

    public static Tile SimpleTile(Vector2 position, string tileSetName, string frameSetName)
    {
        var newTile = new Tile();
        newTile.CurrentAnimation = GlobalSettings.AnimationManager.GetAnimation(tileSetName, frameSetName);
        newTile.Position = position;
        newTile.HitBox = new( position, new Vector2(newTile.CurrentAnimation.CurrentFrame.Width, newTile.CurrentAnimation.CurrentFrame.Height));
        newTile.Size = new Vector2(newTile.CurrentAnimation.CurrentFrame.Width, newTile.CurrentAnimation.CurrentFrame.Height);

        return newTile;

    }
}
