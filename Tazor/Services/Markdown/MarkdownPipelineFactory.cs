using Markdig;
using Tazor.Services.Markdown;

public static class MarkdownPipelineFactory
{
    public static MarkdownPipeline Create(CodeRegionRenderer renderer, CodeRegionExtractor extractor)
    {
        return new MarkdownPipelineBuilder()
            .UseAdvancedExtensions()
            .Use(new CodeRegionExtension(renderer, extractor))
            .Build();
    }
}
