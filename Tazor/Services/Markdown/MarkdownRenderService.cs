using Markdig;
using Markdig.Syntax;
using Microsoft.AspNetCore.Components;

namespace Tazor.Services.Markdown
{
    public class MarkdownRenderService
    {
        private readonly MarkdownPipeline _pipeline;
        private readonly BlazorMarkdownRenderer _renderer;

        public MarkdownRenderService(
            MarkdownPipeline pipeline,
            BlazorMarkdownRenderer renderer)
        {
            _pipeline = pipeline;
            _renderer = renderer;
        }

        public RenderFragment ToRenderFragment(string markdown)
        {
            if (string.IsNullOrWhiteSpace(markdown))
                return builder => { };

            // Parse Markdown into a Markdig AST
            MarkdownDocument document = Markdig.Markdown.Parse(markdown, _pipeline);

            // Convert AST → Blazor RenderFragment
            return _renderer.Render(document);
        }
    }
}
