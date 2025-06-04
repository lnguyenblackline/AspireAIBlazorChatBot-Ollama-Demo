using AspireAIBlazorChatBot.Web;
using AspireAIBlazorChatBot.Web.Components;
using Microsoft.Extensions.AI;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();
builder.Services.AddHttpClient();
builder.AddRedisOutputCache("cache");

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


// Add logging with detailed information
builder.Services.AddLogging();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.AddEventSourceLogger();
builder.Logging.AddFilter("Microsoft", LogLevel.Information);
builder.Logging.AddFilter("System", LogLevel.Information);
builder.Logging.AddFilter("AspireAIBlazorChatBot", LogLevel.Debug);

builder.Services.AddSingleton<ILogger>(static serviceProvider =>
{
    var lf = serviceProvider.GetRequiredService<ILoggerFactory>();
    return lf.CreateLogger(typeof(Program));
});

// register chat client
builder.Services.AddSingleton<IChatClient>(static serviceProvider =>
{
    var logger = serviceProvider.GetRequiredService<ILogger>();
    var config = serviceProvider.GetRequiredService<IConfiguration>();
    var ollamaCnnString = config.GetConnectionString("ollama");
    var defaultLLM = config["Aspire:OllamaSharp:ollama:Models:0"];

    logger.LogInformation("Ollama connection string: {0}", ollamaCnnString);
    logger.LogInformation("Default LLM: {0}", defaultLLM);

    IChatClient chatClient = new OllamaChatClient(new Uri(ollamaCnnString), defaultLLM);

    return chatClient;
});

// register chat nessages
builder.Services.AddSingleton<List<ChatMessage>>(static serviceProvider =>
{
    return new List<ChatMessage>()
    { new ChatMessage(ChatRole.System, "You are a useful assistant that replies using short and precise sentences.")};
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.UseOutputCache();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapDefaultEndpoints();

app.Run();
