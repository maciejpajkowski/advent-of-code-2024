namespace Advent;

public class Day7
{
    public static async Task Solve()
    {
        string responseBody = @"190: 10 19
3267: 81 40 27
83: 17 5
156: 15 6
7290: 6 8 6 15
161011: 16 10 13
192: 17 8 14
21037: 9 7 18 13
292: 11 6 16 20"; //await AdventClient.GetDataForDay(7);
        List<string> lines = responseBody.Split("\n").ToList(); //.SkipLast(1).ToList();
        List<long> results = [];

        foreach (var line in lines)
        {
            results.Add(GetExpectedResult(line));
        }
    }

    private static long GetExpectedResult(string line)
    {
        string[] splitString = line.Split(":");

        return Convert.ToInt32(splitString[0]);
    }
}