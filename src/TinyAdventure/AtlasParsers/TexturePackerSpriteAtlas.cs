using System.Xml.Linq;

namespace TinyAdventure.AtlasParsers;

internal class TexturePackerSpriteAtlas : IAtlasParser
{
    private readonly ParserAttributes _pa = new() {
        ParserName = "TexturePacker Sprite Atlas  Parser",
        RootAtlasNodeName = "TextureAtlas",
        RootAtlasNodeImagePath = "imagePath",
        SpriteNodeName = "sprite",
        SpriteNodeSpriteNameAttribute = "n",
        SpriteNodePositionXAttribute = "x",
        SpriteNodePositionYAttribute = "y",
        SpriteNodeWidthAttribute = "w",
        SpriteNodeHeightAttribute = "h",
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
