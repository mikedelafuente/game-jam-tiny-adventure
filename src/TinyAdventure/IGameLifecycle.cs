using Raylib_cs;

namespace TinyAdventure;

public interface IGameLifecycle
{
    public void Init();

    public void Cleanup();

    public void Update(GameState gameState);

    public void Draw(GameState game, Camera2D camera);
}
