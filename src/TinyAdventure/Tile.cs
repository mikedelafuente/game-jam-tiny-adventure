using System.Numerics;
using Raylib_cs;
using TinyAdventure.Globals;

namespace TinyAdventure;


public class Tile : Entity
{
    private TileAlias TileAlias { get; set; }

    private Tile()
    {

    }

    public static Tile SimpleTile(Vector2 position, TileAlias tileAlias)
    {
        var newTile = new Tile();
        newTile.TileAlias = tileAlias;
        newTile.CurrentAnimation = GlobalSettings.AnimationManager.GetAnimation(tileAlias);
        newTile.Position = position;
        newTile.HitBox = new( position, new Vector2(newTile.CurrentAnimation.CurrentFrame.Width, newTile.CurrentAnimation.CurrentFrame.Height));
        newTile.Size = new Vector2(newTile.CurrentAnimation.CurrentFrame.Width, newTile.CurrentAnimation.CurrentFrame.Height);

        return newTile;
    }

    public static Tile CreateFromEntity(Vector2 position, Tile tile)
    {
        var newTile = new Tile();
        newTile.TileAlias = tile.TileAlias;
        newTile.CurrentAnimation = tile.CurrentAnimation.Clone();
        newTile.Position = position;
        newTile.HitBox = new( position, new Vector2(tile.CurrentAnimation.CurrentFrame.Width, tile.CurrentAnimation.CurrentFrame.Height));  // tile.HitBox; For now, just set the hitbox
        newTile.Size = tile.Size;
        newTile.Flip = tile.Flip;

        return newTile;

    }
}
