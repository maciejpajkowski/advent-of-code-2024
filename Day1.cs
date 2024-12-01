using System.Net.Http.Headers;

using var client = new HttpClient();

try
{
    using HttpResponseMessage response = await client.GetAsync("https://adventofcode.com/2024/day/1/input");
    response.EnsureSuccessStatusCode();
    string responseBody = await response.Content.ReadAsStringAsync();

}
catch (HttpRequestException e)
{
    Console.WriteLine($"Something broke: {e}");
}