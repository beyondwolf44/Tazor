using Markdig.Syntax;
using Markdig.Parsers;

public class CodeRegionBlock : LeafBlock
{
    public string FileLocation { get; set; }
    public string Title { get; set; }
    public string Language { get; set; }

    public CodeRegionBlock(BlockParser parser) : base(parser)
    {
    }
}
