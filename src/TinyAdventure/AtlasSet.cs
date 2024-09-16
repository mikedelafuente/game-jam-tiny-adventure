using Raylib_cs;

namespace TinyAdventure.AtlasParsers;

public class AtlasSet
{
    public Texture2D SpriteSheet;
    public Dictionary<string, AnimationDefinition> Animations = new Dictionary<string, AnimationDefinition>();
}
