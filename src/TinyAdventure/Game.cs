using System.Numerics;
using System.Xml;
using Raylib_cs;

namespace TinyAdventure;

public class Game : IDisposable
{
    private float _zoomMultiplier = 1.0f;
    private readonly float _zoomMultiplierIncrement = 0.1f;

    private readonly float _zoomMultiplierMax = 2.0f;
    private readonly float _zoomMultiplierMin = 0.5f;

    private bool _isCleanedUp = false;


    private float _pixelWindowHeight = 560f;

    private GameState _gameState = new GameState();
    private UI _ui = new UI();
    private Editor _editor = new Editor();



    public void Init()
    {
        LogManager.Trace("Game.Init() started");

        GlobalSettings.Init();
        // The texture manager tracks all loaded textures in the game by being the central point by which we load and unload textures

        // List Atlases you want to use here. You need to know what kind of atlas is in use.
        List<AtlasDefinition> atlases = new List<AtlasDefinition>() {
            new("tile_set_basic1", "assets/textures/objects/super-basic-tilemap/super_basic.xml", '-'),
            new("player1", "assets/textures/character/character_asset_pack.xml", '-')
        };

        // The animation manager initializes all sprite sheets for the game

        GlobalSettings.AnimationManager.Init(atlases, GlobalSettings.TextureManager);
        _gameState.Level.Init();
        _gameState.Player.Init();
        _ui.Init();
        _editor.Init();
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
        _gameState.Level.Cleanup();
        _gameState.Player.Cleanup();
        _ui.Cleanup();
        _editor.Cleanup();

        GlobalSettings.Cleanup();

        _isCleanedUp = true;
        LogManager.Trace("Game.Cleanup() finished");
    }

    public void Update()
    {

        if (Input.DebugPressed()) {
            GlobalSettings.IsDebugMode = !GlobalSettings.IsDebugMode;
        }

        if (Input.ZoomOutPressed()) {
            _zoomMultiplier -= _zoomMultiplierIncrement;
            if (_zoomMultiplier < _zoomMultiplierMin) {
                _zoomMultiplier = _zoomMultiplierMin;
            }
        }

        if (Input.ZoomInPressed()) {
            _zoomMultiplier += _zoomMultiplierIncrement;
            if (_zoomMultiplier > _zoomMultiplierMax) {
                _zoomMultiplier = _zoomMultiplierMax;
            }
        }

        _gameState.Level.Update(_gameState);
        _gameState.Player.Update(_gameState.Level);
        _ui.Update(_gameState);
        _editor.Update(_gameState.Level);

    }

    public void Draw()
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.DarkBlue);

        // Logging in this area will create a massive number of log messages, so be mindful of making calls to log here
        var screenHeight = Raylib.GetScreenHeight();
        var screenWidth = Raylib.GetScreenWidth();

        // var camera = new Camera2D {
        //     Zoom = (screenHeight / _pixelWindowHeight) * _zoomMultiplier,
        //     Offset = { X = screenWidth / 2.0f, Y = screenHeight / 2.0f },
        //     Target = { X = _gameState.Player.Position.X, Y = _gameState.Player.Position.Y }
        // };

        var camera = new Camera2D {
            Zoom = (screenHeight / _pixelWindowHeight) * _zoomMultiplier,
            Offset = { X = screenWidth / 2.0f, Y = screenHeight / 2.0f },
            Target = { X = _gameState.Player.Position.X, Y = _gameState.Player.Position.Y }
        };


        Raylib.BeginMode2D(camera);


        _gameState.Level.Draw(camera);
        _gameState.Player.Draw(camera);
        _ui.Draw(camera);
        _editor.Draw(camera);


        AddInputInfoToDebugBuffer();
        DrawDebugBuffer(camera);


        Raylib.EndMode2D();
        Raylib.EndDrawing();

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
                    $"zoom: {_zoomMultiplier}\n" +
                    $"world_zero: ({worldZero.X:0.00}, {worldZero.Y:0.00})\n" +
                    $"world_edge: ({worldEdge.X:0.00}, {worldEdge.Y:0.00}),\n");

                Raylib.DrawText(GlobalSettings.DebugLogBuffer.ToString(), (int)debugTextPosition.X, (int)debugTextPosition.Y, 10, Color.Black); // Font size adjusted to 20 for visibility
            }
        }
}
