using TinyAdventure.AtlasParsers;

namespace TinyAdventure;

/// <summary>
///
/// </summary>
/// <param name="Alias"></param>
/// <param name="AtlasPath"></param>
/// <param name="NameDelimiter">Used to determine what the delimiter is to indicate this may be a series of images for an animation</param>
public record AtlasDefinition(string Alias, string AtlasPath, char NameDelimiter);

/// <summary>
///
/// </summary>
/// <remarks>
/// The animation manager will make some assumptions about parsing atlases and take actions automatically:
///     - Names should involve a delimiter if you want to split it into animations, such as "-" or "_"
///     - Frames of animations should be prefixed by a delimited such as "_001" or "_1" and must be castable to an int
///     - Periods in names run the risk of having everything dropped after the period as we will trim off the ".png" or ".gif" from names
/// </remarks>
public class AnimationManager : IDisposable
{
    private bool _isCleanedUp = false;

    public Dictionary<string, AtlasSet> AtlasSets = new Dictionary<string, AtlasSet>();

    public List<(string, List<string>)> AtlasNames = new List<(string, List<string>)>();

    public void Init(List<AtlasDefinition> atlases, TextureManager textureManager)
    {
        LogManager.Trace("AnimationManager.Init() started");

        // Iterate through each definition and return some kind of dictionary to go into another dictionary
        LogManager.Trace("There are {0} texture atlases", atlases.Count);

        foreach (var atlasDefinition in atlases) {
            LogManager.Debug("Loading atlas: {0}", atlasDefinition.AtlasPath);
            var atlasParser = AtlasParserFactory.GetParserForFile(atlasDefinition.AtlasPath);
            if (atlasParser != null) {
                var atlasSet = atlasParser.Parse(atlasDefinition.AtlasPath, textureManager, atlasDefinition.NameDelimiter);
                if (IsValidAtlasSet(atlasDefinition.Alias, atlasSet))
                    if (AtlasSets.ContainsKey(atlasDefinition.Alias)) {
                        throw new FileLoadException($"There are multiple definitions for the same Atlas alias [{atlasDefinition.Alias}]. Please give an alternate alias name and try again.");
                    }

                AtlasSets[atlasDefinition.Alias] = atlasSet;
            } else {
                LogManager.Error("No handler has been setup to process the Atlas map: {0} ", null, atlasDefinition.AtlasPath);
            }
        }

        foreach (var atlasAlias in AtlasSets.Keys) {
            var atlasKeys = AtlasSets[atlasAlias].Animations.Keys.ToArray();
            AtlasNames.Add((atlasAlias, atlasKeys.ToList()));
        }
        LogManager.Trace("AnimationManager.Init() finished");
    }


    private static bool IsValidAtlasSet(string alias, AtlasSet? atlasSet)
    {
        if (atlasSet == null) {
            LogManager.Warn("Atlas set [{0}] was null - Atlas is invalid.", alias);
            return false;
        }

        if (atlasSet.SpriteSheet.Id == 0) {
            LogManager.Warn("Atlas [{0}] SpriteSheet does not exist - Atlas is invalid", alias);
            return false;
        }

        if (atlasSet.Animations.Count == 0) {
            LogManager.Warn("Atlas [{0}] does not contain Animations - Atlas is invalid.", alias);
            return false;
        }

        return true;
    }

    public void Cleanup()
    {
        LogManager.Trace("AnimationManager.Cleanup() started");

        _isCleanedUp = true;


        LogManager.Trace("AnimationManager.Cleanup() finished");
    }

    public void Dispose()
    {
        if (!_isCleanedUp) {
            Cleanup();
        }
    }
}
