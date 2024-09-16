namespace TinyAdventure;

public class GameState
{
    public Entity CurrentEntity { get; set; }
    public int CurrentAtlasIndex { get; set; } = 0;
    public int CurrentAnimationIndex { get; set; } = 0;
}
