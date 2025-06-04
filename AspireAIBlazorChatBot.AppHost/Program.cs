var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var ollama = builder.AddOllama(name: "ollama", port: null)
                    .AddModel("phi3.5")
                    .WithOpenWebUI()
                    .WithDataVolume()
                    .PublishAsContainer();


builder.AddProject<Projects.AspireAIBlazorChatBot_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WithReference(ollama);

builder.Build().Run();
