using System.Text.RegularExpressions;

namespace Advent;

public class Day7
{
    private static long totalResult = 0;

    public static async Task Solve()
    {
        string responseBody = await AdventClient.GetDataForDay(7);
        List<string> lines = responseBody.Split("\n").SkipLast(1).ToList();

        foreach (var line in lines)
        {
            var expectedResult = GetExpectedResult(line);
            var testNumbers = GetTestNumbers(line);

            if (IsCalibrationCorrect(expectedResult, testNumbers))
            {
                totalResult += expectedResult;
            }
        }

        Console.WriteLine(totalResult);
    }

    private static long GetExpectedResult(string line)
    {
        return Convert.ToInt64(line.Split(":")[0]);
    }

    private static List<long> GetTestNumbers(string line)
    {
        return line.Split(" ").Skip(1).Select((item) => Convert.ToInt64(item)).ToList();
    }

    private static List<string> GetAllPossibleEquations(List<long> testNumbers, int index)
    {
        List<string> equations = [];

        if (index == testNumbers.Count - 1)
        {
            equations.Add(testNumbers[index].ToString());
            return equations;
        }
        List<string> subCombinations = GetAllPossibleEquations(testNumbers, index + 1);

        foreach (var subCombination in subCombinations)
        {
            foreach (var op in new List<string> { "+", "*" })
            {
                equations.Add(testNumbers[index] + op + subCombination);
            }
        }

        return equations;
    }

    private static List<string> ExtractOperations(List<string> equations)
    {
        List<string> operations = [];

        foreach (var equation in equations)
        {
            MatchCollection matches = Regex.Matches(equation, @"[^\d]");

            string equationOperations = string.Empty;

            foreach (Match match in matches)
            {
                equationOperations += match.Value;
            }

            operations.Add(equationOperations);
        }

        return operations;
    }

    private static bool IsCalibrationCorrect(long expectedResult, List<long> testNumbers)
    {
        var possibleOperations = ExtractOperations(GetAllPossibleEquations(testNumbers, 0));

        foreach (var possibleOperation in possibleOperations)
        {
            var testResult = testNumbers[0];
            for (var i = 1; i < testNumbers.Count; i++)
            {
                if (possibleOperation[i - 1].ToString() == "+")
                {
                    testResult += testNumbers[i];
                }
                else
                {
                    testResult *= testNumbers[i];
                }
            }

            if (testResult == expectedResult) return true;

        }

        return false;
    }
}