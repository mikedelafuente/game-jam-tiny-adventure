using Raylib_cs;

namespace TinyAdventure;

public enum RenderStrategy
{
    TopLeft,
    BottomCenter
}

public class Animation
{
    private float DefaultFrameDurationMs { get; init; }

    private RenderStrategy RenderStrategy { get; init; }
    public AnimationLoopStrategy LoopStrategy { get; set; }

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

    public Animation(Texture2D texture, List<Frame> frames, AnimationLoopStrategy loopStrategy = AnimationLoopStrategy.Forward, float defaultFrameDurationMs = 80, int firstFrameIndex = 0, int lastFrameIndex = -1)
    {
        Texture = texture;
        Frames = frames.ToArray();
        LoopStrategy = loopStrategy;
        DefaultFrameDurationMs = defaultFrameDurationMs >= GlobalSettings.DefaultFrameDurationMsFloor ? defaultFrameDurationMs : GlobalSettings.DefaultFrameDurationMsFloor;

        FirstFrameIndex = firstFrameIndex;
        CurrentFrameIndex = firstFrameIndex;
        LastFrameIndex = lastFrameIndex >= 0 ? lastFrameIndex : Frames.Length - 1;


        if (Frames.Length == 1) {
            LoopStrategy = AnimationLoopStrategy.SingleFrame;
        }
    }
}
