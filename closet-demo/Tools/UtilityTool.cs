using System.ComponentModel;
using ModelContextProtocol.Server;

namespace closet_demo.Tools;

[McpServerToolType]
public class UtilityTool
{
    public enum SpaceType
    {
        [Description("Company")]
        Company = 1,
        [Description("Brand")]
        Brand = 2,
        [Description("Line")]
        Line = 3,
        [Description("Season")]
        Season = 4,
        [Description("Style")]
        Style = 8,
        [Description("Workroom")]
        Workroom = 9,
        [Description("Assortment")]
        Assortment = 50
    }
    
    [McpServerTool(Name = "SearchSpaceHierarchy"), Description("Get the space hierarchy")]
    public static async Task<string> SearchSpaceHierarchy(
        HttpClient client,
        int spaceId,
        SpaceType spaceType)
    {
        var url =
            $"/api/space/hierachy?spaceId={spaceId}&spaceType={spaceType:d}";
        
        var response = await client.GetAsync(url);
        
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }
        
        return "Error: " + response.ReasonPhrase;
    }
}