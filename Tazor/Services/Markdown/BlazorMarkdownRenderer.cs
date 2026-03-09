using Markdig.Syntax;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
public class BlazorMarkdownRenderer
{
    public RenderFragment Render(MarkdownDocument document) => builder =>
    {
        var seq = 0;
        foreach (var node in document)
        {
            RenderNode(builder, ref seq, node);
        }
    };

    private void RenderNode(RenderTreeBuilder builder, ref int seq, MarkdownObject node)
    {
        switch (node)
        {
            case ParagraphBlock paragraph:
                builder.OpenElement(seq++, "p");
                RenderInline(builder, ref seq, paragraph.Inline);
                builder.CloseElement();
                break;

            case HeadingBlock heading:
                var tag = $"h{heading.Level}";
                builder.OpenElement(seq++, tag);
                RenderInline(builder, ref seq, heading.Inline);
                builder.CloseElement();
                break;

            case ListBlock list:
                builder.OpenElement(seq++, list.IsOrdered ? "ol" : "ul");

                foreach (ListItemBlock item in list)
                {
                    builder.OpenElement(seq++, "li");

                    foreach (var subBlock in item)
                    {
                        RenderNode(builder, ref seq, subBlock);
                    }

                    builder.CloseElement();
                }

                builder.CloseElement();
                break;


            // Add more cases as needed (code blocks, images, blockquotes, etc.)
        }
    }

    private void RenderInline(RenderTreeBuilder builder, ref int seq, Markdig.Syntax.Inlines.ContainerInline inline)
    {
        foreach (var child in inline)
        {
            switch (child)
            {
                case Markdig.Syntax.Inlines.LiteralInline literal:
                    builder.AddContent(seq++, literal.Content.ToString());
                    break;

                case Markdig.Syntax.Inlines.EmphasisInline em:
                    builder.OpenElement(seq++, em.DelimiterCount == 2 ? "strong" : "em");
                    RenderInline(builder, ref seq, em);
                    builder.CloseElement();
                    break;

                case Markdig.Syntax.Inlines.LinkInline link:
                    builder.OpenElement(seq++, "a");
                    builder.AddAttribute(seq++, "href", link.Url);
                    RenderInline(builder, ref seq, link);
                    builder.CloseElement();
                    break;

                // Add more inline types as needed
            }
        }
    }
}
