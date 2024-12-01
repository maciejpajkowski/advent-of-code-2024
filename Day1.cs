const string SESSION_COOKIE = "session=53616c7465645f5fa0a6a05092e4eb5e115902dc57e7cfc11d56a6f7d756470203b4c4e9fb8defaf0fd1ad8bba5f6e73391b405f4033556ec9f8f0e5cae41750";
const string INPUT_URL = "https://adventofcode.com/2024/day/1/input";

using var client = new HttpClient();

try
{
    client.DefaultRequestHeaders.Add("cookie", SESSION_COOKIE);
    using HttpResponseMessage response = await client.GetAsync(INPUT_URL);
    response.EnsureSuccessStatusCode();
    string responseBody = await response.Content.ReadAsStringAsync();

    string[] lines = responseBody.Split("\n");
    List<string[]> items = lines.Select((line) => line.Split("   ")).SkipLast(1).ToList();

    List<int> list1 = [];
    List<int> list2 = [];

    foreach (var item in items)
    {
        list1.Add(Convert.ToInt32(item[0]));
        list2.Add(Convert.ToInt32(item[1]));
    }

    /* PART 1 */
    list1.Sort();
    list2.Sort();

    var accumulator = 0;

    for (var i = 0; i < list1.Count; i++)
    {
        accumulator += Math.Abs(list1[i] - list2[i]);
    }

    Console.WriteLine(accumulator); // 2066446

    /* PART 2 */

    int similarityScore = 0;

    foreach (var list1Item in list1)
    {
        int timesItemAppearsInList2 = 0;

        foreach (var list2Item in list2)
        {
            if (list1Item == list2Item)
            {
                timesItemAppearsInList2++;
            }
        }

        similarityScore += list1Item * timesItemAppearsInList2;
    }

    Console.WriteLine(similarityScore); // 24931009

}
catch (HttpRequestException e)
{
    Console.WriteLine($"Something broke: {e}");
}