using System.Net.Http.Headers;

using var client = new HttpClient();

try
{
    client.DefaultRequestHeaders.Add("cookie", "session=53616c7465645f5fa0a6a05092e4eb5e115902dc57e7cfc11d56a6f7d756470203b4c4e9fb8defaf0fd1ad8bba5f6e73391b405f4033556ec9f8f0e5cae41750");
    using HttpResponseMessage response = await client.GetAsync("https://adventofcode.com/2024/day/1/input");
    response.EnsureSuccessStatusCode();
    string responseBody = await response.Content.ReadAsStringAsync();


}
catch (HttpRequestException e)
{
    Console.WriteLine($"Something broke: {e}");
}