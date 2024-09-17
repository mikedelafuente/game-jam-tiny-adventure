namespace TinyAdventure;

public struct GameState
{
    public GameState()
    {
        Player = new Player();
        Level = new Level();
    }

    public Player Player { get; set; }
    public Level Level { get; set; }


}
