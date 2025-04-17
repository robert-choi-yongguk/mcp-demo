using System.ComponentModel;
using ModelContextProtocol.Server;

namespace closet_demo.Tools;

[McpServerToolType]
public class CompanyTool
{
    [McpServerTool(Name = "SearchCompanies"), Description("Get companies by keyword")]
    public static async Task<string> SearchCompanies(
        HttpClient client,
        string keyword = "",
        int pageNo = 1,
        int pageSize = 40)
    {
        var url =
            $"/api/companies?groupId={Constants.GroupId}&sort=0&isDescending=true&pageNo={pageNo}&pageSize={pageSize}&keyword={keyword}";

        var response = await client.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }

        return "Error: " + response.ReasonPhrase;
    }
}