using System.Text.RegularExpressions;

namespace Advent;

public class Day3
{
    private const string mulFindPattern = @"mul\([0-9]{1,3},[0-9]{1,3}\)";
    private const string mulValueGetPattern = @"mul\(([0-9]{1,3}),([0-9]{1,3})\)";

    public async static Task Solve()
    {
        string responseBody = await AdventClient.GetDataForDay(3);

        List<Match> matches = [.. Regex.Matches(responseBody, mulFindPattern, RegexOptions.Multiline).ToList()];

        int accumulator = 0;

        foreach (Match match in matches)
        {
            Match valueMatch = Regex.Match(match.Value, mulValueGetPattern);

            int firstValue = Convert.ToInt32(valueMatch.Groups[1].Value);
            int secondValue = Convert.ToInt32(valueMatch.Groups[2].Value);

            accumulator += firstValue * secondValue;
        }

        Console.WriteLine(accumulator);
    }
}