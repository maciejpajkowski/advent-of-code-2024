using System.Text.RegularExpressions;

namespace Advent;

public class Day7
{
    private static long totalResult = 0;

    public static async Task Solve()
    {
        string responseBody = @"1040: 2 1 8 13 65 5
489775464841: 754 909 829 862 9 4
190: 10 19
3267: 81 40 27
83: 17 5
156: 15 6
7290: 6 8 6 15 // 1 + + + 2 * * * 3 * + + 4 * * + 5 + * + 6 + * * 7 * + * 8 + + *
161011: 16 10 13
192: 17 8 14
21037: 9 7 18 13
292: 11 6 16 20"; //await AdventClient.GetDataForDay(7);
        List<string> lines = responseBody.Split("\n").ToList(); //.SkipLast(1).ToList();
        List<int> results = [];

        foreach (var line in lines)
        {
            if (IsCalibrationCorrect(GetExpectedResult(line), GetTestNumbers(line)))
            {
                totalResult += GetExpectedResult(line);
            }
        }

        Console.WriteLine(totalResult);
    }

    private static int GetExpectedResult(string line)
    {
        return Convert.ToInt32(line.Split(":")[0]);
    }

    private static List<int> GetTestNumbers(string line)
    {
        return line.Split(" ").Skip(1).Select((item) => Convert.ToInt32(item)).ToList();
    }

    private static List<string> GetAllPossibleEquations(List<int> testNumbers, int index)
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

    private static bool IsCalibrationCorrect(int expectedResult, List<int> testNumbers)
    {
        List<char> testedScenarios = [];
        var possibleOperations = ExtractOperations(GetAllPossibleEquations(testNumbers, 0));
        bool wasCorrectCombinationFound = false;

        return false;
    }
}
