using Raylib_cs;

namespace TinyAdventure;

public class TextureManager : IDisposable
{
    private bool _isCleanedUp = false;
    private readonly Dictionary<string, Texture2D> _textures = new();
    public void Init()
    {
        LogManager.Trace("TextureManager.Init() started");

        LogManager.Trace("TextureManager.Init() finished");
    }



    private void AddTexture(string texturePath)
    {
        var texture = Raylib.LoadTexture(texturePath);
        if (texture.Id > 0) {
            _textures.Add(texturePath, texture);
        } else {
            throw new ApplicationException($"Unable to find texture: [{texturePath}]");
        }
    }

    public void Cleanup()
    {
        foreach (var texture in _textures.Values) {
            Raylib.UnloadTexture(texture);
        }

        _isCleanedUp = true;
    }

    public Texture2D GetTexture(string texturePath)
    {
        if (_textures.TryGetValue(texturePath, out var texture)) {
            return texture;
        }

        AddTexture(texturePath);
        return _textures[texturePath];
    }

    public void Dispose()
    {
        // TODO release managed resources here
        if (!_isCleanedUp) {
            Cleanup();
        }
    }
}
