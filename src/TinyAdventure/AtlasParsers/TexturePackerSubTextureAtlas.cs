namespace TinyAdventure.AtlasParsers;

internal class TexturePackerSubTextureAtlas : IAtlasParser
{
    private readonly ParserAttributes _pa = new() {
        ParserName = "TexturePacker SubTexture Atlas Parser",
        RootAtlasNodeName = "TextureAtlas",
        RootAtlasNodeImagePath = "imagePath",
        SpriteNodeName = "SubTexture",
        SpriteNodeSpriteNameAttribute = "name",
        SpriteNodePositionXAttribute = "x",
        SpriteNodePositionYAttribute = "y",
        SpriteNodeWidthAttribute = "width",
        SpriteNodeHeightAttribute = "height",
        SpriteNodeOriginXAttribute = string.Empty,
        SpriteNodeOriginYAttribute = string.Empty,
        SpriteNodeDurationAttribute = string.Empty
    };

    public AtlasSet Parse(string atlasFilePath, TextureManager textureManager, char nameDelimiter)
    {
        return XmlTextureAtlasParser.ParseFile(atlasFilePath, textureManager, _pa, nameDelimiter);
    }

    public bool CanParse(string atlasFilePath)
    {
        return XmlTextureAtlasParser.CanParse(atlasFilePath, _pa);
    }
}
