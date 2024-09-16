using System.Xml.Linq;
using Raylib_cs;

namespace TinyAdventure.AtlasParsers;

internal static class XmlTextureAtlasParser
{
    public static AtlasSet ParseFile(string atlasFilePath, TextureManager textureManager, ParserAttributes pa, char nameDelimiter)
    {
        LogManager.Trace("{0} is parsing the file: {1}", pa.ParserName, atlasFilePath);
        if (!File.Exists(atlasFilePath)) {
            throw new FileNotFoundException("Unable to find the Raylib Atlas file", atlasFilePath);
        }

        var doc = XDocument.Load(atlasFilePath);
        if (doc.Root == null) {
            throw new FileLoadException($"{pa.ParserName} was unable to load a proper XML Document", atlasFilePath);
        }

        var atlas = doc.Element(pa.RootAtlasNodeName);
        if (atlas == null || !string.Equals(atlas.Name.LocalName, pa.RootAtlasNodeName, StringComparison.InvariantCultureIgnoreCase)) {
            throw new FileLoadException($"{pa.ParserName} expected the root element to be {pa.RootAtlasNodeName}", atlasFilePath);
        }

        // Get the image path and load the texture to ensure it is there.
        // The image path should be loaded based on combining the root path of the document with the image path
        var atlasImagePath = atlas.Attribute(pa.RootAtlasNodeImagePath)?.Value;

        if (string.IsNullOrWhiteSpace(atlasImagePath)) {
            throw new FileLoadException($"{pa.ParserName} did not find the 'imagePath' defined in the root element");
        }

        // Determine the full or at least the relative file path -
        FileInfo fi = new FileInfo(atlasFilePath);
        var atlasDirectory = atlasFilePath.Substring(0, atlasFilePath.LastIndexOf(fi.Name, StringComparison.InvariantCultureIgnoreCase));

        var texturePath = Path.Join(atlasDirectory, atlasImagePath);
        Texture2D atlasTexture = textureManager.GetTexture(texturePath);

        // Gather all of the frames first before adding them to animations
        List<Frame> allFrames = new List<Frame>();
        bool isOriginDefined = !string.IsNullOrWhiteSpace(pa.SpriteNodeOriginXAttribute) && !string.IsNullOrWhiteSpace(pa.SpriteNodeOriginYAttribute);

        bool isDurationDefined = !string.IsNullOrWhiteSpace(pa.SpriteNodeDurationAttribute);

        int spriteCount = 0;
        foreach (var spriteElement in atlas.Elements(pa.SpriteNodeName)) {
            spriteCount++;
            LogManager.Trace("Parsing sprite {0} on atlas {1}", spriteCount, atlasFilePath);

            Frame frame = new Frame();

            var frameName = spriteElement.Attribute(pa.SpriteNodeSpriteNameAttribute)?.Value;
            if (frameName != null) {
                frame.Name = CleanName(frameName);
            } else {
                throw new FileLoadException($"A sprite on the atlas is missing a name value [{pa.SpriteNodeSpriteNameAttribute}]", atlasFilePath);
            }

            frame.SetName = GetBaseName(frame.Name, nameDelimiter);

            if (IsAnimationFrame(frame.Name, nameDelimiter)) {
                // Get the frame number
                frame.AnimationFrameNumber = GetFrameNumber(frame.Name, nameDelimiter);
            }

            frame.PositionX = float.Parse(spriteElement.Attribute(pa.SpriteNodePositionXAttribute)!.Value);
            frame.PositionY = float.Parse(spriteElement.Attribute(pa.SpriteNodePositionYAttribute)!.Value);
            frame.Width = float.Parse(spriteElement.Attribute(pa.SpriteNodeWidthAttribute)!.Value);
            frame.Height = float.Parse(spriteElement.Attribute(pa.SpriteNodeHeightAttribute)!.Value);

            if (isOriginDefined) {
                frame.OriginX = float.Parse(spriteElement.Attribute(pa.SpriteNodeOriginXAttribute)!.Value);
                frame.OriginY = float.Parse(spriteElement.Attribute(pa.SpriteNodeOriginYAttribute)!.Value);
            }

            if (isDurationDefined) {
                frame.DefaultDurationMs = int.Parse(spriteElement.Attribute(pa.SpriteNodeOriginYAttribute)!.Value);
            }

            // Build the View Window based on the parsed values
            frame.ViewWindow = new Rectangle(
                frame.PositionX,
                frame.PositionY,
                frame.Width,
                frame.Height
            );

            allFrames.Add(frame);
        }


        // Parse the frames into different animations based on their base names
        AtlasSet atlasSet = new AtlasSet();
        atlasSet.SpriteSheet = atlasTexture;

        foreach (var frame in allFrames) {
            if (atlasSet.Animations.ContainsKey(frame.SetName)) {
                continue;
            }

            if (IsAnimationFrame(frame.Name, nameDelimiter)) {
                // Grab all the animations for the set
                AnimationDefinition animation = new AnimationDefinition();
                animation.Frames = allFrames.Where(f => f.SetName == frame.SetName).OrderBy(f => f.AnimationFrameNumber).ToList();
                atlasSet.Animations.Add(frame.SetName, animation);
            } else {
                AnimationDefinition animation = new AnimationDefinition();
                animation.Frames.Add(frame);
                atlasSet.Animations.Add(frame.SetName, animation);
            }
        }

        return atlasSet;
    }

   public static bool CanParse(string atlasFilePath, ParserAttributes pa)
    {
        try {
            FileInfo fi = new FileInfo(atlasFilePath);
            if (!fi.Extension.Equals(".xml", StringComparison.InvariantCultureIgnoreCase)) {
                return false;
            }

            var doc = XDocument.Load(atlasFilePath);
            if (doc.Root == null) {
                LogManager.Debug("{0} was unable to load a proper XML Document: {1}", pa.ParserName, atlasFilePath);
                return false;
            }

            // Validate the root node and its attributes
            var atlas = doc.Element(pa.RootAtlasNodeName);
            if (atlas == null || !string.Equals(atlas.Name.LocalName, pa.RootAtlasNodeName, StringComparison.InvariantCultureIgnoreCase)) {
                return false;
            }

            if (!atlas.Attributes().Any(att => att.Name.LocalName.Equals(pa.RootAtlasNodeImagePath))) {
                return false;
            }

            // Validate that there is at least one element for the sprites and there must always be the Position and Width/Height attributes
            if (!atlas.Elements().Any(el => el.Name.LocalName.Equals(pa.SpriteNodeName))) {
                return false;
            }

            // Get the first sprite element and assume that all others can be parsed
            var spriteElement = atlas.Elements(pa.SpriteNodeName).First();

            // Create a list of required elements
            string[] requiredAttributeNames = [
                pa.SpriteNodePositionXAttribute,
                pa.SpriteNodePositionYAttribute,
                pa.SpriteNodeWidthAttribute,
                pa.SpriteNodeHeightAttribute
            ];

            foreach (var requiredAttributeName in requiredAttributeNames) {
                if (!spriteElement.Attributes().Any(att => att.Name.LocalName.Equals(requiredAttributeName))) {
                    return false;
                }
            }

        } catch (Exception e) {
            LogManager.Error("Error while determining if the Parser can parse the file", e);
            return false;
        }

        return true;
    }
    private static bool IsAnimationFrame(string frameName, char delimiter)
    {
        if (!frameName.Contains(delimiter)) {
            return false;
        }

        var parts = frameName.Split(delimiter);

        return parts[^1].All(char.IsDigit);
    }

    private static int GetFrameNumber(string frameName, char delimiter)
    {
        if (frameName.Contains(delimiter)) {
            var parts = frameName.Split(delimiter);

            if (parts[^1].All(char.IsDigit)) {
                return int.Parse(parts[^1]);
            }
        }

        LogManager.Warn("Attempted to get FrameNumber for sprite named [{0}], but failed. Used the delimiter [{1}]", frameName, delimiter);
        return -1;
    }

    private static string GetBaseName(string frameName, char delimiter)
    {
        if (!frameName.Contains(delimiter)) {
            return frameName;
        }

        var parts = frameName.Split(delimiter);

        // Check if the last part contains only digits (e.g., 001 in play.run.001.png)
        if (parts[^1].All(char.IsDigit)) {
            // if it is all digits, return everything else concatenated back together
            return string.Join(delimiter, parts.Take(parts.Length - 1));
        }

        // Otherwise, return the name without modifications
        return frameName;
    }

    private static string CleanName(string frameName)
    {
        // if the name is play_run_001.png the result should be play_run_001
        // if the name is play.run.001.png the result should be play.run.001
        // if the name is play.run.001 the result should be play.run.001 because the last part does contain letters

        // Split the name by period
        if (!frameName.Contains('.')) {
            return frameName;
        }

        var parts = frameName.Split('.');

        // Check if the last part contains only digits (e.g., 001 in play.run.001.png)
        var extension = parts[^1]; // This is the syntax to index from the END of the array

        // If the extension
        if (!extension.All(char.IsDigit) && extension.Length == 3) {
            return string.Join(".", parts.Take(parts.Length - 1));
        }

        // Otherwise, return the name without modifications
        return frameName;
    }
}
