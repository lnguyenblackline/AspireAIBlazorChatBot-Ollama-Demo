using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using static System.Console;

namespace SemanticKernel_ConsoleApp;

internal class Program
{
    static async Task Main(string[] args)
    {
       var modelId = "gpt-4o-mini";
       var endpoint = "https://blackline-openai.openai.azure.com/";
       var apiKey = "";

        // Create kernel with Azure OpenAI Chat Completion service
        var builder = Kernel.CreateBuilder()
            .AddAzureOpenAIChatCompletion(modelId, endpoint, apiKey);

        // Add Enterprise components
        builder.Services.AddLogging(services => services.AddConsole().SetMinimumLevel(LogLevel.Trace));
        
        // Build the kernel
        var kernel = builder.Build();
        var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

        // Add a plugin to the kernel
        kernel.Plugins.AddFromType<Plugins.BlackLineTeamPlugin>("Blackline");
        kernel.Plugins.AddFromType<Plugins.WeatherPlugin>("weather");

        // Enable planning
        OpenAIPromptExecutionSettings openAiPromptExecutionSettings = new()
        {
            FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
        };

        // Create a history chat
        var history = new ChatHistory();

        // Initialize the chat with a system message
        string? userInput;
        do
        {
            Write("User >>");
            userInput = ReadLine();

            // Add User Input 
            history.AddUserMessage(userInput);

            //  the response from the chat completion service
            var response =
                await chatCompletionService.GetChatMessageContentAsync(history, openAiPromptExecutionSettings, kernel);
            WriteLine("Assistant >> " + response);
            history.AddMessage(response.Role, response.Content ?? string.Empty);
        }
        while (!string.IsNullOrEmpty(userInput));
    }
}