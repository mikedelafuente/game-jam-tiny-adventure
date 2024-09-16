using System.Numerics;
using Raylib_cs;

namespace TinyAdventure;

/// <summary>
/// An actor is any object which
/// </summary>
public class Entity
{
    public Vector2 Position { get; set; } = Vector2.Zero;

    public Vector2 Size { get; set; } = Vector2.Zero;
    public Rectangle HitBox { get; set; } = new Rectangle(0, 0, 0, 0);
    public bool Flip { get; set; } = false;

    public Animation CurrentAnimation { get; set; }
}
