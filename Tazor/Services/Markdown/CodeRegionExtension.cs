using Markdig;
using Markdig.Renderers;

namespace Tazor.Services.Markdown
{
    public class CodeRegionExtension : IMarkdownExtension
    {
        private readonly CodeRegionRenderer _renderer;
        private readonly CodeRegionExtractor _extractor;

        public CodeRegionExtension(CodeRegionRenderer renderer, CodeRegionExtractor extractor)
        {
            _renderer = renderer;
            _extractor = extractor;
        }

        public void Setup(MarkdownPipelineBuilder pipeline)
        {
            pipeline.BlockParsers.Insert(0, new CodeRegionParser(_extractor));
        }

        public void Setup(MarkdownPipeline pipeline, IMarkdownRenderer renderer)
        {
            // Not used because you render with Blazor
        }
    }
}