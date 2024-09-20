using System.Numerics;
using Raylib_cs;
using TinyAdventure.Globals;

namespace TinyAdventure;

public class Editor
{

    public static bool IsEditing = false;

    public int GridSnapSize { get; set; } = 8;
    public int CurrentAtlasIndex { get; set; } = 0;
    public int CurrentAnimationIndex { get; set; } = 0;
    public Tile CurrentTile { get; set; }

    public void Init()
    {
        CurrentTile = Tile.SimpleTile(Vector2.Zero, Globals.KnownTileSets.SuperBasicTileSet.Tiles.CavernR0C0);
    }
    public void Draw(Camera2D camera, Level level)
    {
        if (IsEditing) {
            DrawCurrentTile(camera, level);
            DrawEditGrid(camera);

        }
    }

    public void DrawCurrentTile(Camera2D camera, Level level)
    {
        // Get mouse position in screen coordinates
        Vector2 mpRaw = Raylib.GetMousePosition();

        // Convert mouse position to world coordinates
        Vector2 mpOrig = Raylib.GetScreenToWorld2D(mpRaw, camera);

        // Snap the original mouse position to the nearest grid increment
        Vector2 mp = Helper.NearestIncrementPoint(mpOrig, GridSnapSize);

        // Draw debug text at a specific screen location converted to world coordinates
        GlobalSettings.DebugLogBuffer.Append($"Mouse: ({mpOrig.X:F2}, {mpOrig.Y:F2})\nSnap World: ({mp.X:F2}, {mp.Y:F2})\n");

        CurrentTile.Position = mp;

        // Draw the texture at the snapped mouse position
        RenderHelper.Draw(CurrentTile);
        //Raylib.DrawTextureV(CurrentTileTexture, mp, Color.White);

        // Input processing for placing a new platform
        if (Input.EditPlacePressed()) {
            bool isOverlapping = false;

            foreach (var (platform, idx) in  level.Tiles.Select((value, index) => (value, index))) {
                if (Raylib.CheckCollisionPointRec(mp, platform.HitBox)) {
                    isOverlapping = true;
                    break;
                }
            }

            if (!isOverlapping) {
                var tile = Tile.CreateFromEntity(mp, CurrentTile);

                level.Tiles.Add(tile);
            }
        }

        // Input processing for removing a platform
        if (Input.EditRemovePressed()) {
            for (int i = 0; i < level.Tiles.Count; i++) {
                if (Raylib.CheckCollisionPointRec(mp, level.Tiles[i].HitBox)) {
                    level.Tiles.RemoveAt(i); // Removes platform at the specified index
                    break;
                }
            }
        }
    }

    private void DrawEditGrid(Camera2D camera)
    {
        float worldSnap = (float)GridSnapSize * camera.Zoom;
        Vector2 worldZero = Raylib.GetScreenToWorld2D(new Vector2(0, 0), camera);
        Vector2 worldEdge = new Vector2((float)Raylib.GetScreenWidth(), (float)Raylib.GetScreenHeight());
        int columns = (int)(Raylib.GetScreenWidth() / worldSnap) + 1;
        int rows = (int)(Raylib.GetScreenHeight() / worldSnap) + 1;

        float columnX = 0f;
        Color gridColor = new Color(0, 0, 0, 20);

        for (int i = 0; i < columns; i++) {
            columnX = (float)Helper.NearestIncrement((int)Raylib.GetScreenToWorld2D(new Vector2(i * worldSnap, 0), camera).X, GridSnapSize);
            Raylib.DrawLineV(new Vector2(columnX, worldZero.Y), new Vector2(columnX, worldEdge.Y), gridColor);
        }

        float rowY = 0f;
        for (int i = 0; i < rows; i++) {
            rowY = (float)Helper.NearestIncrement((int)Raylib.GetScreenToWorld2D(new Vector2(0, i * worldSnap), camera).Y, GridSnapSize);
            Raylib.DrawLineV(new Vector2(worldZero.X, rowY), new Vector2(worldEdge.X, rowY), gridColor);
        }
    }


    public void Update(Level level)
    {

        if (Input.EditPressed()) {
            IsEditing = !IsEditing;
        }

        GlobalSettings.DebugLogBuffer.Append($"IsEditing: {IsEditing}");

        if (IsEditing) {

            if (Input.SaveLevelPressed()) {
                //SaveLevel(state.Level);
            }


            bool changeAnimation = false;
            // Logging in this area will create a massive number of log messages, so be mindful of making calls to log here
            if (Input.EditNextTilePressed()) {
                CurrentAnimationIndex += 1;
                if (CurrentAnimationIndex > GlobalSettings.AnimationManager.AtlasNames[CurrentAtlasIndex].Item2.Count - 1) {
                    CurrentAnimationIndex = 0;
                }

                changeAnimation = true;
            } else if (Input.EditPreviousTilePressed()) {
                CurrentAnimationIndex -= 1;
                if (CurrentAnimationIndex < 0) {
                    CurrentAnimationIndex = GlobalSettings.AnimationManager.AtlasNames[CurrentAtlasIndex].Item2.Count - 1;
                }

                changeAnimation = true;
            } else if (Input.EditPreviousTileSetPressed()) {
                CurrentAtlasIndex -= 1;
                if (CurrentAtlasIndex < 0) {
                    CurrentAtlasIndex = GlobalSettings.AnimationManager.AtlasNames.Count - 1;
                }
                CurrentAnimationIndex = 0;
                changeAnimation = true;
            } else if (Input.EditNextTileSetPressed()) {
                CurrentAtlasIndex += 1;
                if (CurrentAtlasIndex > GlobalSettings.AnimationManager.AtlasNames.Count - 1) {
                    CurrentAtlasIndex = 0;
                }
                CurrentAnimationIndex = 0;
                changeAnimation = true;
            }


            if (Input.EditNextTilePressed()) {

            }

            if (changeAnimation) {
                string atlasName = GlobalSettings.AnimationManager.AtlasNames[CurrentAtlasIndex].Item1;
                string animationName = GlobalSettings.AnimationManager.AtlasNames[CurrentAtlasIndex].Item2[CurrentAnimationIndex];

                CurrentTile.CurrentAnimation = new Animation(GlobalSettings.AnimationManager.AtlasSets[atlasName].SpriteSheet,
                    GlobalSettings.AnimationManager.AtlasSets[atlasName].Animations[animationName].Frames
                );
            }


        }

        AddAtlasNamesToDebugBuffer();

    }

    private void AddAtlasNamesToDebugBuffer()
    {
        var currentAtlas = GlobalSettings.AnimationManager.AtlasNames[CurrentAtlasIndex];
        string atlasName = currentAtlas.Item1;
        string animationName = currentAtlas.Item2[CurrentAnimationIndex];

        GlobalSettings.DebugLogBuffer.Append($"Atlas [{CurrentAtlasIndex + 1}/{GlobalSettings.AnimationManager.AtlasNames.Count}]: {atlasName}\n");
        GlobalSettings.DebugLogBuffer.Append($"Animation [{CurrentAnimationIndex + 1}/{currentAtlas.Item2.Count}]: {animationName}\n");

    }
    public void Cleanup(){}
}
