﻿using System.Numerics;
using Raylib_cs;
using TinyAdventure.Globals;

namespace TinyAdventure;

public enum PlayerAction
{
    Attack,
    Dash,
    Death,
    Fall,
    Hurt,
    Idle,
    Jab,
    Jump,
    Land,
    Climb,
    Pull,
    Push,
    Roll,
    Run,
    Slide,
    Walk
}

public class Player : Entity
{
    public Rectangle FootHitBox { get; set; }
    private float Speed;
    private float JumpForce;
    private Vector2 CurrentVelocity;
    private bool IsGrounded;

    public PlayerAction CurrentAction { get; set; }

    public Player()
    {
    }

    public void Init()
    {
        Position = Vector2.Zero;
        Size = new Vector2(64, 64);
        Speed = (100f / 8.0f) * 12;
        JumpForce = 300;
        CurrentVelocity = Vector2.Zero;
        IsGrounded = false;

        CurrentAction = PlayerAction.Idle;

        // We need to set an animation
        this.CurrentAnimation = GlobalSettings.AnimationManager.GetAnimation(Globals.KnownTileSets.PlayerSet.Tiles.Idle);
        // You can pull the origin from the sprite
        FootHitBox = GetCurrentFootHitBox();
    }

    internal void Draw(Camera2D camera)
    {
        if (GlobalSettings.IsDebugMode) {
            Color hitBoxColor = new Color(0, 255, 0, 100);
            Raylib.DrawRectangleRec(FootHitBox, hitBoxColor);
        }



        RenderHelper.Draw(this);
    }

    internal void Update(Level level)
    {
        var desiredAction = CurrentAction;

        if (Input.LeftPressed()) {
            Flip = true;
            CurrentVelocity.X = -Speed;
            desiredAction = PlayerAction.Run;
        } else if (Input.RightPressed()) {
            Flip = false;
            CurrentVelocity.X = Speed;
            desiredAction = PlayerAction.Run;
        } else {
            CurrentVelocity.X = 0;
            if (CurrentAction != PlayerAction.Land) {
                desiredAction = PlayerAction.Idle;
            }
        }

        CurrentVelocity.Y += level.Gravity * Raylib.GetFrameTime();

        // Calculate Y Velocity
        if (IsGrounded && Input.JumpPressed()) {
            CurrentVelocity.Y = -JumpForce;
            TrySetPlayerAction(PlayerAction.Jump);
        }

        Position += CurrentVelocity * Raylib.GetFrameTime();


        FootHitBox = GetCurrentFootHitBox();

        // Always assume we could be falling
        IsGrounded = false;

        // Check Collisions with the current level
        // Ideally this would only pull in tiles that are in the viewable region.
        foreach (var platform in level.Tiles) {
            if (Raylib.CheckCollisionRecs(FootHitBox, platform.HitBox) && CurrentVelocity.Y > 0f) {
                CurrentVelocity.Y = 0;
                Position = new Vector2(Position.X, platform.Position.Y);
                IsGrounded = true;
            }
        }

        if (IsGrounded == false && CurrentAction != PlayerAction.Jump) {
            desiredAction = PlayerAction.Fall;
        } else if (IsGrounded && (CurrentAction == PlayerAction.Jump || CurrentAction == PlayerAction.Fall)) {
            desiredAction = PlayerAction.Land;
        } else if (CurrentAction == PlayerAction.Land && (CurrentAnimation.CurrentFrameIndex == CurrentAnimation.LastFrameIndex)) {
            desiredAction = PlayerAction.Idle;
        }

        TrySetPlayerAction(desiredAction);
    }

    private Rectangle GetCurrentFootHitBox()
    {
        return new Rectangle(Position.X - (CurrentAnimation.CurrentFrame.Width / 4), Position.Y - (CurrentAnimation.CurrentFrame.Height - CurrentAnimation.CurrentFrame.OriginY - 4.0f), CurrentAnimation.CurrentFrame.Width / 2, 4.0f);
    }

    internal void TrySetPlayerAction(PlayerAction desiredAction)
    {
        if (CurrentAction != desiredAction) {
            if ((CurrentAction == PlayerAction.Fall || CurrentAction == PlayerAction.Jump) && !IsGrounded) return;

            Console.WriteLine($"Action changed to: {desiredAction}");
            CurrentAction = desiredAction; // TODO: Reset the animation?
            switch (CurrentAction) {
                case PlayerAction.Idle:
                    SetCurrentAnimation(KnownTileSets.PlayerSet.Tiles.Idle);
                    break;
                case PlayerAction.Fall:
                    SetCurrentAnimation(KnownTileSets.PlayerSet.Tiles.Fall);
                    break;
                case PlayerAction.Run:
                    SetCurrentAnimation(KnownTileSets.PlayerSet.Tiles.Run);
                    break;
                case PlayerAction.Land:
                    SetCurrentAnimation(KnownTileSets.PlayerSet.Tiles.Land);
                    break;
                case PlayerAction.Jump:
                    SetCurrentAnimation(KnownTileSets.PlayerSet.Tiles.Jump);
                    break;
                default:
                    SetCurrentAnimation(KnownTileSets.PlayerSet.Tiles.Roll);
                    break;
            }
        }
    }

    private void SetCurrentAnimation(TileAlias alias)
    {
        CurrentAnimation = GlobalSettings.AnimationManager.GetAnimation(alias);

    }
    internal void Cleanup()
    {
        // This is handled by the texture manager
        // foreach (var ani in AnimationSet.Values)
        // {
        //     Raylib.UnloadTexture(ani.Texture);
        // }
    }
}
