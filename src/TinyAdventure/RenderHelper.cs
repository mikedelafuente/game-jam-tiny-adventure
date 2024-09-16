using System.Numerics;
using Raylib_cs;

namespace TinyAdventure;

public static class RenderHelper
{
    public static void Draw(Entity entity)
    {
        if (GlobalSettings.IsDebugMode) {
            Color hitBoxColor = new Color(0, 255, 0, 100);
            Raylib.DrawRectangleRec(entity.HitBox, hitBoxColor);
        }

        if (entity.CurrentAnimation.LoopStrategy != AnimationLoopStrategy.SingleFrame) {
            UpdateAnimation(entity.CurrentAnimation);
        }

        DrawAnimation(entity.CurrentAnimation, entity.Position, entity.Flip);
    }

    private static void DrawAnimation(Animation ani, Vector2 position, Boolean flip)
    {
        var _animationWidth = ani.Texture.Width;
        var _animationHeight = ani.Texture.Height;

        var source = ani.CurrentFrame.ViewWindow;

        //var sourceWidth = _animationWidth / _floatNumberOfFrame;
        // var source = new Rectangle
        // {
        //     X = ani.CurrentFrameIndex * sourceWidth,
        //     Y = 0,
        //     Width = sourceWidth,
        //     Height = _animationHeight
        // };


        if (flip) {
            source.Width = -source.Width;
        }

        var dest = new Rectangle {
            X = position.X,
            Y = position.Y,
            Width = source.Width,
            Height = source.Height
        };
        // var dest = new Rectangle
        // {
        //     X = position.X,
        //     Y = position.Y,
        //     Width = _animationWidth / (float)ani.SpriteSheet.NumberOfFrames,
        //     Height = _animationHeight / (float)ani.SpriteSheet.NumberOfRows
        // };

        // if (ani.ZeroPointRenderStrategy == RenderStrategy.BottomCenter)
        // {
        //     Raylib.DrawTexturePro(ani.Texture, source, dest, new Vector2(dest.Width / 2, dest.Height), ani.CurrentRotation, Color.White);
        // }
        // else
        // {
        //     Raylib.DrawTexturePro(ani.Texture, source, dest, Vector2.Zero, ani.CurrentRotation, Color.White);
        // }

        Raylib.DrawTexturePro(ani.Texture, source, dest, new Vector2(ani.CurrentFrame.OriginX, ani.CurrentFrame.OriginY), ani.CurrentRotation, Color.White);
    }

    private static void UpdateAnimation(Animation ani)
    {
        if (!ShouldUpdateAnimation(ani)) return;

        ani.FrameTimer += Raylib.GetFrameTime();

        while (ani.FrameTimer > ani.GetCurrentFrameDurationSeconds()) {
            if (ani.LoopStrategy is AnimationLoopStrategy.Forward
                or AnimationLoopStrategy.ForwardSingle
                or AnimationLoopStrategy.PingPongForward) {
                ani.CurrentFrameIndex += 1;

                if (ani.CurrentFrameIndex == ani.LastFrameIndex + 1) // If we are going beyond the current LastFrame Index
                {
                    if (ani.LoopStrategy == AnimationLoopStrategy.PingPongForward) {
                        ani.CurrentFrameIndex -= 1;
                        ani.LoopStrategy = AnimationLoopStrategy.PingPongBackward;
                    } else if (ani.LoopStrategy == AnimationLoopStrategy.Forward) {
                        ani.CurrentFrameIndex = ani.FirstFrameIndex; // Loop around
                    } else {
                        ani.CurrentFrameIndex = ani.LastFrameIndex;
                    }
                }

                ani.FrameTimer -= ani.GetCurrentFrameDurationSeconds();
            } else if (ani.LoopStrategy is AnimationLoopStrategy.Backward
                       or AnimationLoopStrategy.BackwardSingle
                       or AnimationLoopStrategy.PingPongBackward) {
                ani.CurrentFrameIndex -= 1;
                if (ani.CurrentFrameIndex == ani.LastFrameIndex) {
                    if (ani.LoopStrategy == AnimationLoopStrategy.PingPongBackward) {
                        ani.CurrentFrameIndex += 1;
                        ani.LoopStrategy = AnimationLoopStrategy.PingPongForward;
                    } else if (ani.LoopStrategy == AnimationLoopStrategy.Backward) {
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
        if (ani.LoopStrategy == AnimationLoopStrategy.SingleFrame) return false;

        if (ani.Frames.Length <= 1) return false;

        if (ani.LoopStrategy == AnimationLoopStrategy.None) return false;

        if (ani.LoopStrategy == AnimationLoopStrategy.ForwardSingle && ani.CurrentFrameIndex == ani.LastFrameIndex) return false;

        if (ani.LoopStrategy == AnimationLoopStrategy.BackwardSingle && ani.CurrentFrameIndex == ani.FirstFrameIndex) return false;

        return true;
    }
}
