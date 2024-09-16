namespace TinyAdventure.AtlasParsers;

internal struct ParserAttributes
{
    public required string RootAtlasNodeName { get; set; }
    public required string SpriteNodeName { get; set; }
    public required string SpriteNodeSpriteNameAttribute { get; set; }
    public required string SpriteNodePositionXAttribute { get; set; }
    public required string SpriteNodePositionYAttribute { get; set; }
    public required string SpriteNodeWidthAttribute { get; set; }
    public required string SpriteNodeHeightAttribute { get; set; }
    public required string ParserName { get; set; }
    public required string RootAtlasNodeImagePath { get; set; }
    public string SpriteNodeOriginXAttribute { get; set; }
    public string SpriteNodeOriginYAttribute { get; set; }
    public string SpriteNodeDurationAttribute { get; set; }

}
