namespace Advent;

public class Day4
{
    private static readonly char X = Convert.ToChar("X");
    private static readonly char M = Convert.ToChar("M");
    private static readonly char A = Convert.ToChar("A");
    private static readonly char S = Convert.ToChar("S");

    public async static Task Solve()
    {
        string responseBody = await AdventClient.GetDataForDay(4);
        List<string> lines = responseBody.Split("\n").SkipLast(1).ToList();

        int part1Result = HorizontalOccurrences(lines) + VerticalOccurrences(lines) + DiagonalOccurrences(lines);
        int part2Result = CrossOccurrences(lines);

        Console.WriteLine(part1Result); // 2414
        Console.WriteLine(part2Result); // 1871
    }

    private static int HorizontalOccurrences(List<string> lines)
    {
        int accumulator = 0;

        for (var line = 0; line < lines.Count; line++)
        {
            for (var letter = 0; letter < lines[line].Length; letter++)
            {
                if (lines[line][letter] == X)
                {
                    if (letter > 2 &&
                        lines[line][letter - 1] == M &&
                        lines[line][letter - 2] == A &&
                        lines[line][letter - 3] == S) accumulator++;
                    if (letter < lines[line].Length - 3 &&
                        lines[line][letter + 1] == M &&
                        lines[line][letter + 2] == A &&
                        lines[line][letter + 3] == S) accumulator++;
                }
            }

        }

        return accumulator;
    }

    private static int VerticalOccurrences(List<string> lines)
    {
        int accumulator = 0;

        for (var line = 0; line < lines.Count; line++)
        {
            for (var letter = 0; letter < lines[line].Length; letter++)
            {
                if (lines[line][letter] == X)
                {
                    if (line > 2 &&
                        lines[line - 1][letter] == M &&
                        lines[line - 2][letter] == A &&
                        lines[line - 3][letter] == S) accumulator++;
                    if (line < lines.Count - 3 &&
                        lines[line + 1][letter] == M &&
                        lines[line + 2][letter] == A &&
                        lines[line + 3][letter] == S) accumulator++;
                }
            }
        }

        return accumulator;
    }

    private static int DiagonalOccurrences(List<string> lines)
    {
        int accumulator = 0;

        for (var line = 0; line < lines.Count; line++)
        {
            for (var letter = 0; letter < lines[line].Length; letter++)
            {
                if (lines[line][letter] == X)
                {
                    // to top-left
                    if (line > 2 && letter > 2 &&
                        lines[line - 1][letter - 1] == M &&
                        lines[line - 2][letter - 2] == A &&
                        lines[line - 3][letter - 3] == S) accumulator++;
                    // to top-right
                    if (line > 2 && letter < lines[line].Length - 3 &&
                        lines[line - 1][letter + 1] == M &&
                        lines[line - 2][letter + 2] == A &&
                        lines[line - 3][letter + 3] == S) accumulator++;
                    // to bottom-left
                    if (line < lines.Count - 3 && letter > 2 &&
                        lines[line + 1][letter - 1] == M &&
                        lines[line + 2][letter - 2] == A &&
                        lines[line + 3][letter - 3] == S) accumulator++;
                    // to bottom-right
                    if (line < lines.Count - 3 && letter < lines[line].Length - 3 &&
                        lines[line + 1][letter + 1] == M &&
                        lines[line + 2][letter + 2] == A &&
                        lines[line + 3][letter + 3] == S) accumulator++;
                }
            }
        }

        return accumulator;
    }

    private static int CrossOccurrences(List<string> lines)
    {
        int accumulator = 0;

        for (var line = 1; line < lines.Count - 1; line++)
        {
            for (var letter = 1; letter < lines[line].Length - 1; letter++)
            {
                if (lines[line][letter] == A)
                {
                    // M on the left, S on the right
                    if (lines[line - 1][letter - 1] == M &&
                        lines[line + 1][letter - 1] == M &&
                        lines[line - 1][letter + 1] == S &&
                        lines[line + 1][letter + 1] == S) accumulator++;
                    // M on the right, S on the left
                    if (lines[line - 1][letter + 1] == M &&
                        lines[line + 1][letter + 1] == M &&
                        lines[line - 1][letter - 1] == S &&
                        lines[line + 1][letter - 1] == S) accumulator++;
                    // M on the top, S on the bottom
                    if (lines[line - 1][letter + 1] == M &&
                        lines[line + 1][letter + 1] == S &&
                        lines[line - 1][letter - 1] == M &&
                        lines[line + 1][letter - 1] == S) accumulator++;
                    // M on the bottom, S on the top
                    if (lines[line - 1][letter + 1] == S &&
                        lines[line + 1][letter + 1] == M &&
                        lines[line - 1][letter - 1] == S &&
                        lines[line + 1][letter - 1] == M) accumulator++;
                }
            }
        }

        return accumulator;
    }


}

