using Markdig;
using Microsoft.AspNetCore.Components;
public class MarkdownRenderService
{
    private readonly MarkdownPipeline _pipeline = MarkdownPipelineFactory.Create();
    private readonly BlazorMarkdownRenderer _renderer = new();

    public RenderFragment ToRenderFragment(string markdown)
    {
        var doc = Markdig.Markdown.Parse(markdown, _pipeline);
        return _renderer.Render(doc);
    }
}
