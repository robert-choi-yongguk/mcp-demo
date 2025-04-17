// See https://aka.ms/new-console-template for more information


using OpenAI;
using OpenAI.Chat;


var httpClient = new HttpClient();
var url = "https://medium.com/@mutluozkurt/creating-an-mcp-server-and-client-with-net-a-step-by-step-guide-0c3833dde3c4";
string content = await httpClient.GetStringAsync(url);

ChatMessage[] messages =
[
    ChatMessage.CreateUserMessage("Briefly summarize the following downloaded content:"),
    ChatMessage.CreateUserMessage(content),
];
        
ChatCompletionOptions options = new()
{
    MaxOutputTokenCount = 256,
    Temperature = 0.8f,
};

var chatClient = new OpenAIClient(
        "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")
    .GetChatClient("gpt-4o-mini");
var result = await chatClient.CompleteChatAsync(
    messages, options);

Console.WriteLine($"{result}");