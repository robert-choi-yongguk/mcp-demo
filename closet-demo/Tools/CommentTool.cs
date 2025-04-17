using System.ComponentModel;
using ModelContextProtocol.Server;

namespace closet_demo.Tools;

[McpServerToolType]
public class CommentTool
{
    [McpServerTool(Name = "SearchStyleComments"), Description("Add a comment to a style")]
    public static async Task<string> SearchComments(
        HttpClient client,
        string styleId,
        int version)
    {
        var url =
            $"/rest/api/styles/{styleId}/versions/{version}/comments";
        
        var response = await client.GetAsync(url);
        
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }
        
        return "Error: " + response.ReasonPhrase;
    }
}