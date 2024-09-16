using System.Xml.Linq;
using Raylib_cs;

namespace TinyAdventure.AtlasParsers;

internal class RaylibAtlas : IAtlasParser
{
    private readonly ParserAttributes _pa = new() {
        ParserName = "Raylib Atlas Parser",
        RootAtlasNodeName = "AtlasTexture",
        RootAtlasNodeImagePath = "imagePath",
        SpriteNodeName = "Sprite",
        SpriteNodeSpriteNameAttribute = "nameId",
        SpriteNodePositionXAttribute = "positionX",
        SpriteNodePositionYAttribute = "positionY",
        SpriteNodeWidthAttribute = "sourceWidth",
        SpriteNodeHeightAttribute = "sourceHeight",
        SpriteNodeOriginXAttribute = "originX",
        SpriteNodeOriginYAttribute = "originY",
        SpriteNodeDurationAttribute = string.Empty,


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
