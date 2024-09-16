using System.Numerics;
using System.Xml;
using Raylib_cs;

namespace TinyAdventure;

public class Game : IDisposable
{
    private bool _isCleanedUp = false;

    private TextureManager _textureManager = new TextureManager();
    private AnimationManager _animationManager = new AnimationManager();

    private GameState _gameState = new GameState();

    private float _pixelWindowHeight = 560f;

    public void Init()
    {
        LogManager.Trace("Game.Init() started");

        // The texture manager tracks all loaded textures in the game by being the central point by which we load and unload textures
        _textureManager.Init();

        // List Atlases you want to use here. You need to know what kind of atlas is in use.
        List<AtlasDefinition> atlases = new List<AtlasDefinition>() {
            new("atlasTexturePackerExample", "assets/atlasTexturePacker.xml", '-'),
            new("atlasTextureRaylibExample", "assets/atlasRaylib.xml", '_'),
            new("enemies_sheet", "assets/textures/objects/platformer-assets-base/enemies_sheet.xml", '_'),
            new("tiles_sheet", "assets/textures/objects/platformer-assets-base/tiles_sheet.xml", '_'),
        };

        // The animation manager initializes all sprite sheets for the game
        _animationManager.Init(atlases, _textureManager);

        string atlasName = _animationManager.AtlasNames[_gameState.CurrentAtlasIndex].Item1;
        string animationName = _animationManager.AtlasNames[_gameState.CurrentAtlasIndex].Item2[_gameState.CurrentAnimationIndex];

        _gameState.CurrentEntity = new Entity();
        _gameState.CurrentEntity.Position = Vector2.Zero;
        _gameState.CurrentEntity.CurrentAnimation = new Animation(_animationManager.AtlasSets[atlasName].SpriteSheet,
            _animationManager.AtlasSets[atlasName].Animations[animationName].Frames
            );


        LogManager.Trace("Game.Init() finished");

    }

    public void Dispose()
    {
        LogManager.Trace("Game.Dispose() started");
        // TODO release managed resources here
        if (!_isCleanedUp)
        {
            Cleanup();
        }
        LogManager.Trace("Game.Dispose() finished");
    }

    public void Cleanup()
    {
        LogManager.Trace("Game.Cleanup() started");

        _animationManager.Cleanup();

        _isCleanedUp = true;
        LogManager.Trace("Game.Cleanup() finished");
    }

    public void Update()
    {
        bool changeAnimation = false;
        // Logging in this area will create a massive number of log messages, so be mindful of making calls to log here
        if (Raylib.IsKeyPressed(KeyboardKey.Up)) {
            _gameState.CurrentAnimationIndex += 1;
            if (_gameState.CurrentAnimationIndex > _animationManager.AtlasNames[_gameState.CurrentAtlasIndex].Item2.Count - 1) {
                _gameState.CurrentAnimationIndex = 0;
            }

            changeAnimation = true;
        } else if (Raylib.IsKeyPressed(KeyboardKey.Down)) {
            _gameState.CurrentAnimationIndex -= 1;
            if (_gameState.CurrentAnimationIndex < 0) {
                _gameState.CurrentAnimationIndex = _animationManager.AtlasNames[_gameState.CurrentAtlasIndex].Item2.Count - 1;
            }

            changeAnimation = true;
        } else if (Raylib.IsKeyPressed(KeyboardKey.Left)) {
            _gameState.CurrentAtlasIndex -= 1;
            if (_gameState.CurrentAtlasIndex < 0) {
                _gameState.CurrentAtlasIndex = _animationManager.AtlasNames.Count - 1;
            }
            _gameState.CurrentAnimationIndex = 0;
            changeAnimation = true;
        } else if (Raylib.IsKeyPressed(KeyboardKey.Right)) {
            _gameState.CurrentAtlasIndex += 1;
            if (_gameState.CurrentAtlasIndex > _animationManager.AtlasNames.Count - 1) {
                _gameState.CurrentAtlasIndex = 0;
            }
            _gameState.CurrentAnimationIndex = 0;
            changeAnimation = true;
        }

        if (changeAnimation) {
            string atlasName = _animationManager.AtlasNames[_gameState.CurrentAtlasIndex].Item1;
            string animationName = _animationManager.AtlasNames[_gameState.CurrentAtlasIndex].Item2[_gameState.CurrentAnimationIndex];

            _gameState.CurrentEntity.CurrentAnimation = new Animation(_animationManager.AtlasSets[atlasName].SpriteSheet,
                _animationManager.AtlasSets[atlasName].Animations[animationName].Frames
            );
        }
    }

    public void Draw()
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.DarkBlue);

        // Logging in this area will create a massive number of log messages, so be mindful of making calls to log here
        var screenHeight = Raylib.GetScreenHeight();
        var screenWidth = Raylib.GetScreenWidth();

        float _zoomMultiplier = 1.0f;

        // var camera = new Camera2D {
        //     Zoom = (screenHeight / _pixelWindowHeight) * _zoomMultiplier,
        //     Offset = { X = screenWidth / 2.0f, Y = screenHeight / 2.0f },
        //     Target = { X = _gameState.Player.Position.X, Y = _gameState.Player.Position.Y }
        // };

        var camera = new Camera2D {
            Zoom = (screenHeight / _pixelWindowHeight) * _zoomMultiplier,
            Offset = { X = screenWidth / 2.0f, Y = screenHeight / 2.0f },
            Target = { X = _gameState.CurrentEntity.Position.X, Y = _gameState.CurrentEntity.Position.Y }
        };


        Raylib.BeginMode2D(camera);

        RenderHelper.Draw(_gameState.CurrentEntity);


        AddInputInfoToDebugBuffer();
        AddAtlasNamesToDebugBuffer();
        DrawDebugBuffer(camera);


        Raylib.EndMode2D();
        Raylib.EndDrawing();

    }


    private void AddAtlasNamesToDebugBuffer()
    {
        var currentAtlas = _animationManager.AtlasNames[_gameState.CurrentAtlasIndex];
        string atlasName = currentAtlas.Item1;
        string animationName = currentAtlas.Item2[_gameState.CurrentAnimationIndex];

        GlobalSettings.DebugLogBuffer.Append($"Atlas [{_gameState.CurrentAtlasIndex + 1}/{_animationManager.AtlasNames.Count}]: {atlasName}\n");
        GlobalSettings.DebugLogBuffer.Append($"Animation [{_gameState.CurrentAnimationIndex + 1}/{currentAtlas.Item2.Count}]: {animationName}\n");

    }
    private void AddInputInfoToDebugBuffer()
        {
            if (GlobalSettings.IsDebugMode) {
                int gamepad = 0; // Assuming gamepad 0 is the first gamepad

                if (Raylib.IsGamepadAvailable(gamepad)) {
                    // Get the status of XYAB buttons
                    bool xButtonPressed = Raylib.IsGamepadButtonDown(gamepad, GamepadButton.RightFaceLeft);
                    bool yButtonPressed = Raylib.IsGamepadButtonDown(gamepad, GamepadButton.RightFaceUp);
                    bool aButtonPressed = Raylib.IsGamepadButtonDown(gamepad, GamepadButton.RightFaceDown);
                    bool bButtonPressed = Raylib.IsGamepadButtonDown(gamepad, GamepadButton.RightFaceRight);

                    // Get the status of triggers
                    float leftTrigger = Raylib.GetGamepadAxisMovement(gamepad, GamepadAxis.LeftTrigger);
                    float rightTrigger = Raylib.GetGamepadAxisMovement(gamepad, GamepadAxis.RightTrigger);

                    // Get the left joystick
                    float leftStickX = Raylib.GetGamepadAxisMovement(gamepad, GamepadAxis.LeftX);
                    float leftStickY = Raylib.GetGamepadAxisMovement(gamepad, GamepadAxis.LeftY);

                    Vector2 leftStickNormalizedVector = Vector2.Normalize(new Vector2(leftStickX, leftStickY));

                    // Append the information to the debug log
                    GlobalSettings.DebugLogBuffer.AppendLine($"Gamepad {gamepad} input:");
                    GlobalSettings.DebugLogBuffer.AppendLine($"X Button Pressed: {xButtonPressed}");
                    GlobalSettings.DebugLogBuffer.AppendLine($"Y Button Pressed: {yButtonPressed}");
                    GlobalSettings.DebugLogBuffer.AppendLine($"A Button Pressed: {aButtonPressed}");
                    GlobalSettings.DebugLogBuffer.AppendLine($"B Button Pressed: {bButtonPressed}");
                    GlobalSettings.DebugLogBuffer.AppendLine($"Left Trigger: {leftTrigger:0.00}");
                    GlobalSettings.DebugLogBuffer.AppendLine($"Right Trigger: {rightTrigger:0.00}");
                    GlobalSettings.DebugLogBuffer.AppendLine($"Left Stick X: N: {leftStickNormalizedVector.X} A: {leftStickX:0.00}");
                    GlobalSettings.DebugLogBuffer.AppendLine($"Left Stick Y: N: {leftStickNormalizedVector.Y} A: {leftStickY:0.00}");
                } else {
                    GlobalSettings.DebugLogBuffer.AppendLine("Gamepad not available.");
                }
            }
        }

        private void DrawDebugBuffer(Camera2D camera)
        {
            // Debug text drawing - disabled by default
            if (GlobalSettings.IsDebugMode) {
                Vector2 worldZero = Raylib.GetScreenToWorld2D(new Vector2(0, 0), camera);
                Vector2 worldEdge = new Vector2((float)Raylib.GetScreenWidth(), (float)Raylib.GetScreenHeight());

                // Draw a background:
                float edgeOffset = 10f;
                Vector2 debugTextPosition = Raylib.GetScreenToWorld2D(new Vector2(20.0f, 20.0f), camera);
                if (GlobalSettings.ShowDebugMask) {
                    Vector2 debugBackgroundPosition = Raylib.GetScreenToWorld2D(new Vector2(edgeOffset, edgeOffset), camera);
                    Vector2 debugBackgroundSize = new Vector2((worldEdge.X - (edgeOffset * 2)) / camera.Zoom, (worldEdge.Y - (edgeOffset * 2)) / camera.Zoom);
                    Raylib.DrawRectangleV(debugBackgroundPosition, debugBackgroundSize, new Color(255, 255, 255, 80));
                }

                GlobalSettings.DebugLogBuffer.Append(
                    $"world_zero: ({worldZero.X:0.00}, {worldZero.Y:0.00})\n" +
                    $"world_edge: ({worldEdge.X:0.00}, {worldEdge.Y:0.00}),\n");

                Raylib.DrawText(GlobalSettings.DebugLogBuffer.ToString(), (int)debugTextPosition.X, (int)debugTextPosition.Y, 10, Color.Black); // Font size adjusted to 20 for visibility
            }
        }
}
