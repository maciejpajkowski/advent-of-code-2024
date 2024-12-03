namespace Advent;

public class Day2
{
    public async static Task Solve()
    {
        string responseBody = await AdventClient.GetDataForDay(2);
        List<string> lines = [.. responseBody.Split("\n")];
        List<string[]> reports = lines.Select((line) => line.Split(" ")).SkipLast(1).ToList();

        int safeScore = 0;
        int safeScoreWithDampener = 0;

        foreach (var report in reports)
        {
            if (IsSafe(report)) safeScore++;
            if (IsSafeWithProblemDampener(report)) safeScoreWithDampener++;
        }

        Console.WriteLine($"safeScore: {safeScore}");
        Console.WriteLine($"safeScoreWithDampener: {safeScoreWithDampener}");
    }

    private static bool IsSafe(string[] report)
    {
        var levels = report.Select((item) => Convert.ToInt32(item)).ToList();

        if (AreLevelsRepeated(levels)) return false;
        if (IsLevelsOrderIncorrect(levels)) return false;
        if (IsLevelsDifferenceTooHigh(levels)) return false;

        return true;
    }

    private static bool IsSafeWithProblemDampener(string[] report)
    {
        var levels = report.Select((item) => Convert.ToInt32(item)).ToList();

        if (!IsSafe(report))
        {
            bool[,] safetyRecord = new bool[3, levels.Count];

            for (var i = 0; i < levels.Count; i++)
            {
                var dampenedLevels = levels.ToList();
                dampenedLevels.RemoveAt(i);
                safetyRecord[0, i] = AreLevelsRepeated(dampenedLevels);
                safetyRecord[1, i] = IsLevelsOrderIncorrect(dampenedLevels);
                safetyRecord[2, i] = IsLevelsDifferenceTooHigh(dampenedLevels);

                if (!safetyRecord[0, i] && !safetyRecord[1, i] && !safetyRecord[2, i]) return true;
            }
        }

        return false;
    }

    private static bool AreLevelsRepeated(List<int> levels)
    {
        var uniques = levels.Distinct().ToList();
        return uniques.Count != levels.Count;
    }

    private static bool IsLevelsOrderIncorrect(List<int> levels)
    {
        var orderedLevels = levels.ToList();
        orderedLevels.Sort(levels[0] - levels[1] > 0 ? (a, b) => b - a : (a, b) => a - b);

        for (var i = 0; i < levels.Count; i++)
        {
            if (orderedLevels[i] != levels[i]) return true;
        }

        return false;
    }

    private static bool IsLevelsDifferenceTooHigh(List<int> levels)
    {
        for (var i = 0; i < levels.Count; i++)
        {
            if (i + 1 == levels.Count) continue;

            if (Math.Abs(levels[i] - levels[i + 1]) > 3) return true;
        }

        return false;
    }

}