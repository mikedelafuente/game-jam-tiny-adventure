using System.Numerics;
using Raylib_cs;
using TinyAdventure.Globals;

namespace TinyAdventure;

public static class RenderHelper
{
    public static void Draw(Entity entity)
    {
        if (GlobalSettings.IsDebugMode) {
            Color hitBoxColor = new Color(0, 255, 0, 100);
            Raylib.DrawRectangleRec(entity.HitBox, hitBoxColor);

        }

        if (entity.CurrentAnimation.Strategy != AnimationStrategy.SingleFrame) {
            UpdateAnimation(entity.CurrentAnimation);
        }

        DrawAnimation(entity.CurrentAnimation, entity.Position, entity.Flip);
    }

    private static void DrawAnimation(Animation ani, Vector2 position, Boolean flip)
    {
        var _animationWidth = ani.Texture.Width;
        var _animationHeight = ani.Texture.Height;

        var source = ani.CurrentFrame.ViewWindow;

        float width = source.Width;
        if (flip) {

            source.Width = -source.Width;
        }

        var dest = new Rectangle {
            X = position.X,
            Y = position.Y,
            Width = width,
            Height = source.Height
        };


        Raylib.DrawTexturePro(ani.Texture, source, dest, new Vector2(ani.CurrentFrame.OriginX, ani.CurrentFrame.OriginY), ani.CurrentRotation, Color.White);
    }

    private static void UpdateAnimation(Animation ani)
    {
        if (!ShouldUpdateAnimation(ani)) return;

        ani.FrameTimer += Raylib.GetFrameTime();

        while (ani.FrameTimer > ani.GetCurrentFrameDurationSeconds()) {
            if (ani.Strategy is AnimationStrategy.Forward
                or AnimationStrategy.ForwardSingle
                or AnimationStrategy.PingPongForward) {
                ani.CurrentFrameIndex += 1;

                if (ani.CurrentFrameIndex == ani.LastFrameIndex + 1) // If we are going beyond the current LastFrame Index
                {
                    if (ani.Strategy == AnimationStrategy.PingPongForward) {
                        ani.CurrentFrameIndex -= 1;
                        ani.Strategy = AnimationStrategy.PingPongBackward;
                    } else if (ani.Strategy == AnimationStrategy.Forward) {
                        ani.CurrentFrameIndex = ani.FirstFrameIndex; // Loop around
                    } else {
                        ani.CurrentFrameIndex = ani.LastFrameIndex;
                    }
                }

                ani.FrameTimer -= ani.GetCurrentFrameDurationSeconds();
            } else if (ani.Strategy is AnimationStrategy.Backward
                       or AnimationStrategy.BackwardSingle
                       or AnimationStrategy.PingPongBackward) {
                ani.CurrentFrameIndex -= 1;
                if (ani.CurrentFrameIndex == ani.LastFrameIndex) {
                    if (ani.Strategy == AnimationStrategy.PingPongBackward) {
                        ani.CurrentFrameIndex += 1;
                        ani.Strategy = AnimationStrategy.PingPongForward;
                    } else if (ani.Strategy == AnimationStrategy.Backward) {
                        ani.CurrentFrameIndex = ani.LastFrameIndex;
                    } else {
                        ani.CurrentFrameIndex = ani.FirstFrameIndex;
                    }
                }

                ani.FrameTimer -= ani.GetCurrentFrameDurationSeconds();
            }
        }
    }


    private static bool ShouldUpdateAnimation(Animation ani)
    {
        if (ani.Strategy == AnimationStrategy.SingleFrame) return false;

        if (ani.Frames.Length <= 1) return false;

        if (ani.Strategy == AnimationStrategy.None) return false;

        if (ani.Strategy == AnimationStrategy.ForwardSingle && ani.CurrentFrameIndex == ani.LastFrameIndex) return false;

        if (ani.Strategy == AnimationStrategy.BackwardSingle && ani.CurrentFrameIndex == ani.FirstFrameIndex) return false;

        return true;
    }
}
