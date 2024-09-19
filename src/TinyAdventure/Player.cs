using System.Numerics;
using Raylib_cs;

namespace TinyAdventure;

public enum PlayerAction
{
    Idle,
    Run,
    Jump,
    Fall,
    Land
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
        CurrentAction = PlayerAction.Idle;


        // We need to set an animation
        this.CurrentAnimation = GlobalSettings.AnimationManager.GetAnimation("player1", "player-run");
        // You can pull the origin from the sprite
        FootHitBox = new Rectangle(Position.X - 4, Position.Y + CurrentAnimation.CurrentFrame.OriginY - 4, 8.0f, 4.0f);
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


        FootHitBox = new Rectangle(Position.X - (CurrentAnimation.CurrentFrame.Width/4) , Position.Y - (CurrentAnimation.CurrentFrame.Height - CurrentAnimation.CurrentFrame.OriginY - 4.0f), CurrentAnimation.CurrentFrame.Width/2, 4.0f);

        IsGrounded = false;

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

    internal void TrySetPlayerAction(PlayerAction desiredAction)
    {
        if (CurrentAction != desiredAction) {
            if ((CurrentAction == PlayerAction.Fall || CurrentAction == PlayerAction.Jump) && !IsGrounded) return;

            Console.WriteLine($"Action changed to: {desiredAction}");
            CurrentAction = desiredAction;  // TODO: Reset the animation?
        }
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
