using System.ComponentModel;
using System.Net.Http.Json;
using closet_demo.Helpers;
using ModelContextProtocol.Server;

namespace closet_demo.Tools;

[McpServerToolType]
public class WorkroomTool
{
    public enum CreateRoomType
    {
        [Description("A room created directly under a season.")]
        SeasonChild = 5,

        [Description("A child room created under a room.")]
        RoomChild = 9,
    }

    [McpServerTool(Name = "SearchWorkrooms"), Description("Get workrooms by keyword")]
    public static async Task<string> SearchWorkrooms(
        HttpClient client,
        string keyword = "",
        int pageNo = 1,
        int pageSize = 40)
    {
        var url =
            $"/api/rooms?groupId={Constants.GroupId}&sort=0&isDescending=true&pageNo={pageNo}&pageSize={pageSize}&keyword={keyword}";

        var response = await client.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }

        return "Error: " + response.ReasonPhrase;
    }

    [McpServerTool(Name = "CreateWorkroom"), Description("Create a new workroom")]
    public static async Task<string> CreateWorkroom(
        HttpClient client,
        string name,
        int parentId,
        CreateRoomType type)
    {
        var url = "/api/rooms";

        var response = await client.PostAsync(url, JsonContent.Create(new { name, parentId, type = (int)type }));

        if (response.IsSuccessStatusCode)
        {
            var newRoomId = await response.Content.ReadAsStringAsync(); 
            return ClosetPathHelper.GetWorkroomPath(newRoomId);
        }

        return "Error: " + response.ReasonPhrase;
    }
}