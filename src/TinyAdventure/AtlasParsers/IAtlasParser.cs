namespace TinyAdventure.AtlasParsers;

internal interface IAtlasParser
{
    /// <summary>
    /// Parses the passed in file to an Atlas set and populates the texture manager with the texture
    /// </summary>
    /// <param name="atlasFilePath"></param>
    /// <param name="textureManager"></param>
    /// <param name="nameDelimiter">The delimiter to determine the frame name such as '_' for player_run_0003 or player-run_003</param>
    /// <returns></returns>
    AtlasSet Parse(string atlasFilePath, TextureManager textureManager, char nameDelimiter);

    /// <summary>
    /// Determines if the given parser can parse the given atlas file
    /// </summary>
    /// <param name="atlasFilePath"></param>
    /// <returns></returns>
    bool CanParse(string atlasFilePath);
}
