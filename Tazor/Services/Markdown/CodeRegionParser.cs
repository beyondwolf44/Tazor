using Markdig.Parsers;
using Markdig.Syntax;
using System.Text.RegularExpressions;
using Tazor.Services.Markdown;
public class CodeRegionParser : BlockParser
{
    private static readonly Regex Pattern =
        new(@"\[:::FileLocation=""(?<file>[^""]+)""\s+Title=""(?<title>[^""]+)""\s+Language=""(?<lang>[^""]+)"":::\]",
            RegexOptions.Compiled);

    private readonly CodeRegionExtractor _extractor;

    public CodeRegionParser(CodeRegionExtractor extractor)
    {
        OpeningCharacters = new[] { '[' };
        _extractor = extractor;
    }

    public override BlockState TryOpen(BlockProcessor processor)
    {
        var line = processor.Line.ToString();

        var match = Pattern.Match(line);
        if (!match.Success)
            return BlockState.None;

        var file = match.Groups["file"].Value;
        var title = match.Groups["title"].Value;
        var lang = match.Groups["lang"].Value;

        var code = _extractor.ExtractRegion(file, title);

        var block = new CodeRegionBlock(this)
        {
            FileLocation = file,
            Title = title,
            Language = lang,
            Code = code
        };

        processor.NewBlocks.Push(block);

        return BlockState.BreakDiscard;
    }
}
