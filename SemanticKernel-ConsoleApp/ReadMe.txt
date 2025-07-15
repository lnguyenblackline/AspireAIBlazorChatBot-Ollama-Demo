<<< AI >>
1.
How to set Up LLM
A- Knowledge
    https://www.reddit.com/r/LocalLLaMA/comments/18hzun0/sharing_a_simple_local_llm_setup/
    https://github.com/ollama/ollama?tab=readme-ov-file
    https://github.com/microsoft/generative-ai-for-beginners/tree/main/15-rag-and-vector-databases
B - Practice
1. Aspire
https://www.youtube.com/watch?v=UtSSMs6ObqY ==> maybe not very helpful.
https://www.youtube.com/watch?v=4B3ppx2U8bE ==> Milan
https://www.youtube.com/watch?v=J-Vy3pKaXS0

2. WebApi:
https://blog.antosubash.com/posts/ollama-with-extension-ai-and-function-calling

3. Sematic-Kernel:
https://www.youtube.com/watch?v=4pI-LxK-NwE   ==> DotNetCore Central
A - Configuration:
   - Set up Auzre Open AI in Ai.Azure. 
B - Implementation:
  Steps:
  a. Nuget:
      Microsoft.SemanticKernel ==> important
      Microsoft.Extensions.DependencyInjection
      Microsoft.Extensions.Logging
      Microsoft.Extensions.Logging.Console
  b. Implementation:
    base-url: https://ai.azure.com/resource/overview?wsid=/subscriptions/135f69f0-f956-42fb-9e98-a335d353976f/resourceGroups/blc-internal/providers/Microsoft.CognitiveServices/accounts/blackline-openai&tid=9698df9e-4907-4fa6-a1d0-b8939c999dc1
    apiKey:
    Ex:
    var modelId = "gpt-4o-mini";
    var endpoint = "https://blackline-openai.openai.azure.com/";
    var apiKey = "6f19c652baa6456093af806313332059";
    repos: https://github.com/lnguyenblackline/AspireAIBlazorChatBot-Ollama-Demo


>>> Overall Note:
- BlackLine-AiRepos: https://github.com/lnguyenblackline/AspireAIBlazorChatBot-Ollama-Demo/tree/master/AspireAIBlazorChatBot.Web
<< Done >>

