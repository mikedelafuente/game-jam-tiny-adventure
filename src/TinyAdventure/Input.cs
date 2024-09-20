using Raylib_cs;
using TinyAdventure.Globals;

namespace TinyAdventure;

    internal static class Input
    {
            private static bool wasGamepadRightFaceDownReleased = true;


            public static bool ShowDebugKeyMap = false;

            public static bool LeftPressed()
            {
                if (Raylib.IsGamepadAvailable(0)) {
                    if (Raylib.GetGamepadAxisMovement(0, GamepadAxis.LeftX) < -GlobalSettings.GamepadDeadZone) {
                        return true;
                    }
                }
                return Raylib.IsKeyDown(KeyboardKey.Left);
            }

            public static bool RightPressed()
            {
                if (Raylib.IsGamepadAvailable(0)) {
                    if (Raylib.GetGamepadAxisMovement(0, GamepadAxis.LeftX) > GlobalSettings.GamepadDeadZone) {
                        return true;
                    }
                }
                return Raylib.IsKeyDown(KeyboardKey.Right);
            }

            public static bool UpPressed()
            {
                if (Raylib.IsGamepadAvailable(0)) {
                    if (Raylib.GetGamepadAxisMovement(0, GamepadAxis.LeftY) > GlobalSettings.GamepadDeadZone) {
                        return true;
                    }
                }
                return Raylib.IsKeyDown(KeyboardKey.Up);
            }

            public static bool DownPressed()
            {
                if (Raylib.IsGamepadAvailable(0)) {
                    if (Raylib.GetGamepadAxisMovement(0, GamepadAxis.LeftY) < -GlobalSettings.GamepadDeadZone) {
                        return true;
                    }
                }
                return Raylib.IsKeyDown(KeyboardKey.Down);
            }

            public static bool JumpPressed()
            {
                if (Raylib.IsGamepadAvailable(0))
                {
                    bool currentJumpButtonState = Raylib.IsGamepadButtonDown(0, GamepadButton.RightFaceDown);
                    if (currentJumpButtonState && wasGamepadRightFaceDownReleased)
                    {
                        wasGamepadRightFaceDownReleased = false; // Mark as not released
                        return true;
                    }
                    if (!currentJumpButtonState)
                    {
                        wasGamepadRightFaceDownReleased = true; // Button is released
                    }
                }
                return Raylib.IsKeyPressed(KeyboardKey.Space);
            }

            public static bool GrabPressed()
            {
                return Raylib.IsKeyPressed(KeyboardKey.LeftControl);
            }

            public static bool MenuPressed()
            {
                if (Raylib.IsGamepadAvailable(0)) {
                    if (Raylib.IsGamepadButtonDown(0, GamepadButton.MiddleRight)) {
                        return true;
                    }
                }
                return Raylib.IsKeyPressed(KeyboardKey.Escape);
            }

            public static bool DebugPressed()
            {
                return Raylib.IsKeyPressed(KeyboardKey.Grave);
            }

            public static bool EditPressed()
            {
                return Raylib.IsKeyPressed(KeyboardKey.F2);
            }

            public static bool EditPlacePressed()
            {
                return Raylib.IsMouseButtonPressed(MouseButton.Left);
            }

            public static bool EditRemovePressed()
            {
                return Raylib.IsMouseButtonPressed(MouseButton.Right);
            }

            public static bool ZoomOutPressed()
            {
                return Raylib.IsKeyPressed(KeyboardKey.Z);
            }

            public static bool ZoomInPressed()
            {
                return Raylib.IsKeyPressed(KeyboardKey.X);
            }


            public static bool SaveLevelPressed()
            {
                return Raylib.IsKeyPressed(KeyboardKey.S);
            }

            public static bool EditNextTilePressed()
            {
                return Raylib.IsKeyPressed(KeyboardKey.RightBracket);
            }

            public static bool EditPreviousTilePressed()
            {
                return Raylib.IsKeyPressed(KeyboardKey.LeftBracket);
            }

            public static bool EditNextTileSetPressed()
            {
                return Raylib.IsKeyPressed(KeyboardKey.Equal);
            }


            public static bool EditPreviousTileSetPressed()
            {
                return Raylib.IsKeyPressed(KeyboardKey.Minus);
            }
    }
