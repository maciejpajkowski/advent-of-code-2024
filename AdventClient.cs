namespace Advent;

public class AdventClient
{
    private static readonly HttpClient client = new();

    public static async Task<string> GetData(int day)
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
        return $"session=53616c7465645f5fa0a6a05092e4eb5e115902dc57e7cfc11d56a6f7d756470203b4c4e9fb8defaf0fd1ad8bba5f6e73391b405f4033556ec9f8f0e5cae41750";
    }
}