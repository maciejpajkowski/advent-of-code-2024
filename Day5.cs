namespace Advent;

public class Requirements
{
    public readonly HashSet<string> requiredBefore = [];
    public readonly HashSet<string> requiredAfter = [];
}

public class Day5
{
    private static readonly Dictionary<string, Requirements> ruleset = [];
    private static readonly List<string> rules = [];
    private static readonly List<string> updates = [];

    public async static Task Solve()
    {
        string responseBody = await AdventClient.GetDataForDay(5);
        List<string> lines = responseBody.Split("\n").SkipLast(1).ToList();

        BuildRulesAndUpdates(lines);
        BuildRuleset(rules);

        List<string> validUpdates = [];
        List<string> reorderedUpdates = [];

        foreach (var update in updates)
        {
            if (IsUpdateValid(update))
            {
                validUpdates.Add(update);
            }
            else
            {
                reorderedUpdates.Add(ReorderUpdate(update));
            }
        }

        Console.WriteLine(CalculateResult(validUpdates));
        Console.WriteLine(CalculateResult(reorderedUpdates));
    }

    private static void BuildRulesAndUpdates(List<string> lines)
    {
        foreach (var line in lines)
        {
            if (line.Contains(Convert.ToChar("|")))
            {
                rules.Add(line);
            }

            if (line.Contains(Convert.ToChar(",")))
            {
                updates.Add(line);
            }
        }

    }

    private static void BuildRuleset(List<string> rules)
    {

        foreach (var rule in rules)
        {
            var splitValues = rule.Split("|");
            var firstValue = splitValues[0];
            var secondValue = splitValues[1];

            if (!ruleset.ContainsKey(firstValue))
            {
                ruleset.Add(firstValue, new Requirements());
            }

            if (!ruleset.ContainsKey(secondValue))
            {
                ruleset.Add(secondValue, new Requirements());
            }

            ruleset[firstValue].requiredAfter.Add(secondValue);
            ruleset[secondValue].requiredBefore.Add(firstValue);
        }
    }

    private static bool IsUpdateValid(string update)
    {
        List<string> pages = [.. update.Split(",")];

        for (var currPageIndex = 0; currPageIndex < pages.Count; currPageIndex++)
        {
            var currentPage = pages[currPageIndex];

            for (var testedPageIndex = 0; testedPageIndex < pages.Count; testedPageIndex++)
            {
                var testedPage = pages[testedPageIndex];

                if (currPageIndex == testedPageIndex) continue;

                if (testedPageIndex < currPageIndex && ruleset[currentPage].requiredAfter.Contains(testedPage)) return false;
                if (testedPageIndex > currPageIndex && ruleset[currentPage].requiredBefore.Contains(testedPage)) return false;
            }
        }

        return true;
    }

    private static string ReorderUpdate(string update)
    {
        List<string> pages = [.. update.Split(",")];

        for (var currPageIndex = 0; currPageIndex < pages.Count; currPageIndex++)
        {
            var currentPage = pages[currPageIndex];

            for (var testedPageIndex = 0; testedPageIndex < pages.Count; testedPageIndex++)
            {
                var testedPage = pages[testedPageIndex];

                if (currPageIndex == testedPageIndex) continue;

                if (!ruleset.ContainsKey(currentPage)) continue;

                if (
                    testedPageIndex < currPageIndex && ruleset[currentPage].requiredAfter.Contains(testedPage) ||
                    testedPageIndex > currPageIndex && ruleset[currentPage].requiredBefore.Contains(testedPage))
                {
                    (pages[testedPageIndex], pages[currPageIndex]) = (pages[currPageIndex], pages[testedPageIndex]);
                    currPageIndex = 0;
                    currentPage = pages[currPageIndex];
                    testedPageIndex = -1;
                }
            }
        }

        return string.Join(",", [.. pages]);

    }

    private static int CalculateResult(List<string> updates)
    {
        int accumulator = 0;

        foreach (var update in updates)
        {
            var pages = update.Split(",");
            var length = pages.Length;
            var middlePageIndex = (int)MathF.Floor(length / 2);

            accumulator += Convert.ToInt32(pages[middlePageIndex]);
        }

        return accumulator;
    }
}