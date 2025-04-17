using System.ComponentModel;
using ModelContextProtocol.Server;

namespace closet_demo.Tools;

[McpServerToolType]
public class StyleTool
{
    public enum CreateStyleType
    {
        [Description("A single style created directly under a room.")]
        Single = 0,
        [Description("A style created under a room with a turntable.")]
        Turntable = 1,
        [Description("A style created under a room with a blank.")]
        Blank = 2,
        [Description("A style created under a room with a multi style.")]
        Multi = 3
    }
    
    [McpServerTool(Name = "SearchStyles"), Description("Get styles by keyword")]
    public static async Task<string> SearchStyles(
        HttpClient client,
        string keyword = "",
        int pageNo = 1,
        int pageSize = 40)
    {
        var url =
            $"/api/styles?groupId={Constants.GroupId}&sort=0&isDescending=true&pageNo={pageNo}&pageSize={pageSize}";
        
        var response = await client.GetAsync(url);
        
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }
        
        return "Error: " + response.ReasonPhrase;
    }
    
    [McpServerTool(Name = "CreateStyle"), Description("Create a new style")]
    public static async Task<string> CreateStyle(
        HttpClient client,
        string name,
        int roomId,
        string filePath,
        CreateStyleType fileType)
    {
        var url = "/api/styles";

        var streamContent = new StreamContent(
            new FileStream(filePath, FileMode.Open));
        streamContent.Headers.Add("Content-Type", "application/octet-stream");
        using MultipartContent content = new MultipartFormDataContent
        {
            { new StringContent(name), "styleNumber" },
            { new StringContent(roomId.ToString()), "roomId" },
            { new StringContent($"{fileType:d}"), "fileType" },
            { streamContent, "file", Path.GetFileName(filePath) }
        };
        
        var response = await client.PostAsync(url, content);
        
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }
        
        return "Error: " + response.ReasonPhrase;
    }
    
    [McpServerTool(Name = "GetDownloadUrl"), Description("Get the download URL of a style")]
    public static async Task<string> GetDownloadUrl(
        HttpClient client,
        string styleId,
        int version)
    {
        // /api/styles/styleId/versions/version/downloadUrl
        var url =
            $"/api/styles/{styleId}/versions/{version}/downloadUrl";
        
        var response = await client.GetAsync(url);
        
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }
        
        return "Error: " + response.ReasonPhrase;
    }
}