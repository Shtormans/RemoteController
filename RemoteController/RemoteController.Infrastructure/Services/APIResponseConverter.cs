using Newtonsoft.Json;
using System.Text;

namespace RemoteController.Infrastructure.Services;

public static class APIResponseConverter
{
    public static async Task<T> DeserializeResponse<T>(HttpResponseMessage response)
    {
        var contentString = await response.Content.ReadAsStringAsync();

        var objResponse = JsonConvert.DeserializeObject<T>(contentString);

        return objResponse!;
    }

    public static StringContent SerializeResponse<T>(T obj)
    {
        var json = JsonConvert.SerializeObject(obj);

        var content = new StringContent(json, Encoding.UTF8, "application/json");

        return content;
    }
}
