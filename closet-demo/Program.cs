using System.Net.Http.Headers;
using closet_demo.Tools;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = Host.CreateApplicationBuilder(args);
builder.Logging.AddConsole(consoleLogOptions => { consoleLogOptions.LogToStandardErrorThreshold = LogLevel.Trace; });
builder.Services
    .AddMcpServer(option =>
    {
        option.ServerInfo = new()
        {
            Name = "Closet OpenAPI MCP Server",
            Version = "1.0.0",
        };
    })
    .WithTools<WorkroomTool>()
    .WithTools<StyleTool>()
    .WithTools<CompanyTool>()
    .WithTools<BrandTool>()
    .WithTools<UtilityTool>()
    .WithStdioServerTransport()
    .WithToolsFromAssembly();

builder.Services.AddSingleton(_ =>
{
    var client = new HttpClient
    {
        BaseAddress = new Uri(Constants.ApiHost)
    };

    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
        "Bearer", "76b55cbc9b084c77a8e867fad9b4b303");
    client.DefaultRequestHeaders.Add("X-User-Email", "opzerg@naver.com");
    client.DefaultRequestHeaders.Add("Api-Version", "2");
    client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("closet-mcp", "1.0"));

    return client;
});

await builder.Build().RunAsync();