using Raylib_cs;

namespace TinyAdventure;

public struct Frame
{
    /// <summary>
    /// For animations, this will often equate to a trimmed value if the name, otherwise it will just be the name
    /// For example, player_run_001 will result in naming to be based on
    /// </summary>
    public string SetName { get; set; }

    /// <summary>
    /// The given Name of the sprite
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Conceptually a view window is an isolated rectangle to overlay and isolate the sprite on the sprite sheet
    /// </summary>
    public Rectangle ViewWindow { get; set; }

    /// <summary>
    /// In an animation, how long should the frame be shown for in milliseconds - combine this with other multipliers to make faster or slower overall animations
    /// </summary>
    public float DefaultDurationMs { get; set; }

    /// <summary>
    /// The upper-left X position of the frame on the spritesheet
    /// </summary>
    public float PositionX { get; set; }

    /// <summary>
    /// The upper-left Y position of the frame on the spritesheet
    /// </summary>
    public float PositionY { get; set; }

    /// <summary>
    /// Within the actual source frame, where is the "origin" X point at (often 0,0 - but sometimes we define at the bottom center or center)
    /// </summary>
    public float OriginX { get; set; }

    /// <summary>
    /// Within the actual source frame, where is the "origin" Y point at (often 0,0 - but sometimes we define at the bottom center or center)
    /// </summary>
    public float OriginY { get; set; }

    /// <summary>
    /// The width of the frame on the sprite sheet
    /// </summary>
    public float Width { get; set; }

    /// <summary>
    /// The height of the frame on the sprite sheet
    /// </summary>
    public float Height { get; set; }

    /// <summary>
    /// If this is part of an animation, this will contain a frame number. If it was badly parsed, it will be -1.
    /// </summary>
    public int AnimationFrameNumber { get; set; }


    /// <summary>
    /// Creates a copy of this Frame.
    /// </summary>
    /// <returns>A new Frame with the same values.</returns>
    public Frame Clone()
    {
        return new Frame
        {
            SetName = this.SetName,
            Name = this.Name,
            ViewWindow = new Rectangle(this.ViewWindow.X, this.ViewWindow.Y, this.ViewWindow.Width, this.ViewWindow.Height),
            DefaultDurationMs = this.DefaultDurationMs,
            PositionX = this.PositionX,
            PositionY = this.PositionY,
            OriginX = this.OriginX,
            OriginY = this.OriginY,
            Width = this.Width,
            Height = this.Height,
            AnimationFrameNumber = this.AnimationFrameNumber
        };
    }
}
