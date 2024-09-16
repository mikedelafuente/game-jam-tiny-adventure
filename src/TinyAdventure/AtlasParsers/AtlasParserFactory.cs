namespace TinyAdventure.AtlasParsers;

internal static class AtlasParserFactory
{
    private static List<IAtlasParser> _atlasParsers = new List<IAtlasParser>();

    /// <summary>
    /// Gets the appropriate parser for the file based on the contents of the passed in file.
    /// This minimizes the overhead for developers to determine the "right" parser
    /// </summary>
    /// <param name="atlasFilePath"></param>
    /// <returns></returns>
    /// <exception cref="FileLoadException"></exception>
    public static IAtlasParser? GetParserForFile(string atlasFilePath)
    {
        if (_atlasParsers.Count == 0) {
            _atlasParsers.Add(new TexturePackerSpriteAtlas());
            _atlasParsers.Add(new TexturePackerSubTextureAtlas());
            _atlasParsers.Add(new RaylibAtlas());
        }

        if (!File.Exists(atlasFilePath)) {
            throw new FileNotFoundException("The Atlas file does not exist", atlasFilePath);
        }

        // Determine if this is JSON or XML
        foreach (var parser in _atlasParsers) {
            if (parser.CanParse(atlasFilePath)) {
                return parser;
            }
        }

        return null;
    }
}
