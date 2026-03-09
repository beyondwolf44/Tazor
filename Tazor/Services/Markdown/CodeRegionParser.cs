using Markdig.Parsers;
using Markdig.Syntax;
using System.Text.RegularExpressions;

public class CodeRegionParser : BlockParser
{
    private static readonly Regex Pattern =
        new(@"\[:::FileLocation=""(?<file>[^""]+)""\s+Title=""(?<title>[^""]+)""\s+Language=""(?<lang>[^""]+)"":::\]",
            RegexOptions.Compiled);



    public CodeRegionParser()
    {
        OpeningCharacters = new[] { '[' };
    }

    public override BlockState TryOpen(BlockProcessor processor)
    {
        var line = processor.Line.ToString();
       

        var match = Pattern.Match(line);
        
        if (!match.Success)
            return BlockState.None;

        var block = new CodeRegionBlock(this)
        {
            FileLocation = match.Groups["file"].Value,
            Title = match.Groups["title"].Value,
            Language = match.Groups["lang"].Value
        };

        processor.NewBlocks.Push(block);
        return BlockState.BreakDiscard;
    }
}
