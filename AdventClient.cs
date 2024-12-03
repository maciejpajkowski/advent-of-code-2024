namespace Advent;

public class AdventClient
{
    private static readonly HttpClient client = new();

    public static async Task<string> GetDataForDay(int day)
    {
        try
        {
            client.DefaultRequestHeaders.Add("cookie", GetCookie());
            using HttpResponseMessage response = await client.GetAsync(GetInputUrl(day));
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Something broke :/ {e}");
            return string.Empty;
        }
    }

    private static string GetInputUrl(int day)
    {
        return $"https://adventofcode.com/2024/day/{day}/input";
    }


    private static string GetCookie()
    {
        return $"session=COOKIE_HERE";
    }
}