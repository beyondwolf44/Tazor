using Tazor.Components;
using Tazor.Services.Markdown;
using Markdig;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddSingleton<CodeRegionExtractor>(sp =>
    new CodeRegionExtractor("../../TestBlazorApp\\Components"));

builder.Services.AddSingleton<CodeRegionRenderer>();
builder.Services.AddSingleton<BlazorMarkdownRenderer>();

builder.Services.AddSingleton<MarkdownPipeline>(sp =>
{
    var regionRenderer = sp.GetRequiredService<CodeRegionRenderer>();
    var regionExtractor = sp.GetRequiredService<CodeRegionExtractor>();
    return MarkdownPipelineFactory.Create(regionRenderer, regionExtractor);
});

builder.Services.AddSingleton<MarkdownRenderService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
