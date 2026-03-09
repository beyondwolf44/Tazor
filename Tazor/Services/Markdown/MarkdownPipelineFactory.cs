using Markdig;

public static class MarkdownPipelineFactory
{
    public static MarkdownPipeline Create()
    {
        return new MarkdownPipelineBuilder()
            .UseAdvancedExtensions()
            .Build();
    }
}
