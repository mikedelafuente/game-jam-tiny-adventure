using Raylib_cs;
using TinyAdventure.Globals;

namespace TinyAdventure;

public enum RenderStrategy
{
    TopLeft,
    BottomCenter
}

public class Animation
{
    public float DefaultFrameDurationMs { get; init; }

    public RenderStrategy RenderStrategy { get; init; }
    public AnimationStrategy Strategy { get; set; }

    public Frame[] Frames { get; init; } = [];

    public Texture2D Texture { get; init; }

    public int CurrentFrameIndex = 0;

    public Frame CurrentFrame => Frames[CurrentFrameIndex];

    public int FirstFrameIndex = -1;

    public int LastFrameIndex = -1;

    public float GetCurrentFrameDurationSeconds()
    {
        return (CurrentFrame.DefaultDurationMs > 0 ? CurrentFrame.DefaultDurationMs : DefaultFrameDurationMs) / 1000;
    }

    public float CurrentRotation = 0f;

    public float FrameTimer = 0f;


    public Animation(Texture2D texture, List<Frame> frames, AnimationStrategy strategy = AnimationStrategy.Forward, float defaultFrameDurationMs = 80, int firstFrameIndex = 0, int lastFrameIndex = -1)
    {
        Texture = texture;
        Frames = frames.ToArray();
        Strategy = strategy;
        DefaultFrameDurationMs = defaultFrameDurationMs >= GlobalSettings.DefaultFrameDurationMsFloor ? defaultFrameDurationMs : GlobalSettings.DefaultFrameDurationMsFloor;

        FirstFrameIndex = firstFrameIndex;
        CurrentFrameIndex = firstFrameIndex;
        LastFrameIndex = lastFrameIndex >= 0 ? lastFrameIndex : Frames.Length - 1;


        if (Frames.Length == 1) {
            Strategy = AnimationStrategy.SingleFrame;
        }
    }

    public Animation Clone()
    {
        // Create a deep copy of the frames
        var clonedFrames = Frames.Select(frame => frame.Clone()).ToList();

        // Return a new instance of Animation with the same properties
        return new Animation(Texture, clonedFrames, Strategy, DefaultFrameDurationMs, FirstFrameIndex, LastFrameIndex)
        {
            CurrentFrameIndex = this.CurrentFrameIndex,
            CurrentRotation = this.CurrentRotation,
            FrameTimer = this.FrameTimer
        };
    }

}
