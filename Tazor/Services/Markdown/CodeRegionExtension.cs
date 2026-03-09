using Markdig;
using Markdig.Renderers;
using Tazor.Services.Markdown;

namespace Tazor.Services.Markdown
{
    /// <summary>
    /// Registers the CodeRegionParser and CodeRegionRenderer with Markdig.
    /// </summary>
    public class CodeRegionExtension : IMarkdownExtension
    {
        private readonly CodeRegionRenderer _renderer;

        public CodeRegionExtension(CodeRegionRenderer renderer)
        {
            _renderer = renderer;
        }

        // Register the parser (this runs BEFORE Markdown is parsed)
        public void Setup(MarkdownPipelineBuilder pipeline)
        {
            // Insert at the top so it catches your custom syntax early
            pipeline.BlockParsers.Insert(0, new CodeRegionParser());
        }

        // Register the renderer (this runs AFTER Markdown is parsed)
        public void Setup(MarkdownPipeline pipeline, IMarkdownRenderer renderer)
        {
            // IMPORTANT:
            // Markdig's renderer is for HTML, but you're using a Blazor renderer.
            // So we do NOT add anything here.
            //
            // Your BlazorMarkdownRenderer handles CodeRegionBlock directly.
        }
    }
}
