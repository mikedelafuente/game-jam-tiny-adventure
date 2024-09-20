using TinyAdventure.Globals;

namespace TinyAdventure;

public record TileAlias(int Id, string Alias, TileSetAlias TileSet,  AnimationStrategy Strategy, float FramesPerSecond, int FirstFrameIndex = 0, int LastFrameIndex = -1);
