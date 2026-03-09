using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
namespace Tazor.Services.Markdown
{
    public class CodeRegionRenderer
    {
        private readonly CodeRegionExtractor _extractor;

        public CodeRegionRenderer(CodeRegionExtractor extractor)
        {
            _extractor = extractor;
        }

        public void Render(RenderTreeBuilder builder, ref int seq, CodeRegionBlock block)
        {
            var code = _extractor.ExtractRegion(block.FileLocation, block.Title);

            builder.OpenElement(seq++, "pre");
            builder.OpenElement(seq++, "code");

            if (!string.IsNullOrWhiteSpace(block.Language))
            {
                builder.AddAttribute(seq++, "class", $"language-{block.Language}");
            }

            builder.AddContent(seq++, code);

            builder.CloseElement(); // code
            builder.CloseElement(); // pre
        }
    }
}
