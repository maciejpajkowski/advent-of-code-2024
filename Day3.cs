using System.Text.RegularExpressions;

namespace Advent;

public class Day3
{
    private const string mulFindPattern = @"mul\(\d+,\d+\)";
    private const string mulValueGetPattern = @"mul\((\d+),(\d+)\)";
    private const string mulDoDontPattern = @"mul\((\d+),(\d+)\)|do\(\)|don't\(\)";

    public async static Task Solve()
    {
        string responseBody = await AdventClient.GetDataForDay(3);

        SolvePart1(responseBody);
        SolvePart2(responseBody);
    }

    private static void SolvePart1(string responseBody)
    {
        List<Match> matches = [.. Regex.Matches(responseBody, mulFindPattern, RegexOptions.Multiline).ToList()];

        int accumulator = 0;

        foreach (Match match in matches)
        {
            accumulator += GetMultipliedValuesFromMatch(match);
        }

        Console.WriteLine(accumulator);
    }

    private static void SolvePart2(string responseBody)
    {
        List<Match> matches = [.. Regex.Matches(responseBody, mulDoDontPattern, RegexOptions.Multiline).ToList()];

        int accumulator = 0;
        bool shouldDo = true;

        for (var i = 0; i < matches.Count; i++)
        {
            if (matches[i].Value.Contains("mul"))
            {
                if (shouldDo)
                {
                    accumulator += GetMultipliedValuesFromMatch(matches[i]);
                }
            }
            else
            {
                shouldDo = matches[i].Value.Contains("do()");
            }
        }

        Console.WriteLine(accumulator);

    }

    private static int GetMultipliedValuesFromMatch(Match match)
    {

        Match valueMatch = Regex.Match(match.Value, mulValueGetPattern);

        int firstValue = Convert.ToInt32(valueMatch.Groups[1].Value);
        int secondValue = Convert.ToInt32(valueMatch.Groups[2].Value);

        return firstValue * secondValue;

    }
}