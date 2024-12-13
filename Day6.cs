namespace Advent;


public class Day6
{
    private enum Direction
    {
        UP,
        RIGHT,
        DOWN,
        LEFT
    }

    private static Direction currentDirection = Direction.UP;
    private static int X = 0;
    private static int Y = 0;
    private static bool hasGuardLeft = false;
    private static readonly HashSet<string> originalPath = [];

    public async static Task Solve()
    {
        string responseBody = await AdventClient.GetDataForDay(6);
        List<string> map = responseBody.Split("\n").SkipLast(1).ToList();
        List<string> reusableMap = [.. map];

        GetInitialCoordinates(map);

        /* PART 1 */
        while (!hasGuardLeft)
        {
            MoveGuard(ref map, true);
        }

        Console.WriteLine(CalculateVisitedPositions(map));

        /* PART 2 */
        var possibleLoops = 0;

        foreach (var ogPathCoords in originalPath.Skip(1))
        {
            var coords = ogPathCoords.Split("-");
            List<string> tempMap = [.. reusableMap];
            List<string> pathRecord = [];
            PlaceObstacleAt(Convert.ToInt32(coords[0]), Convert.ToInt32(coords[1]), ref tempMap);

            GetInitialCoordinates(tempMap);
            hasGuardLeft = false;

            while (!hasGuardLeft)
            {
                // doesnt work yet
                if (IsGuardStuckInLoop(ref pathRecord))
                {
                    possibleLoops++;
                    break;
                }

                pathRecord.Add($"{X}-{Y}-{currentDirection}");

                MoveGuard(ref tempMap, false);
            }

        }
        Console.WriteLine(possibleLoops);
    }

    private static void GetInitialCoordinates(List<string> map)
    {
        for (int x = 0; x < map[0].Length; x++)
        {
            for (int y = 0; y < map.Count; y++)
            {
                if (map[y][x].ToString() == "^")
                {
                    X = x;
                    Y = y;
                    currentDirection = Direction.UP;
                }
            }
        }
    }

    private static void CheckForObstacle(List<string> map)
    {
        if (Y > 0 && currentDirection == Direction.UP && map[Y - 1][X] == Convert.ToChar("#"))
        {
            currentDirection = Direction.RIGHT;
            CheckForObstacle(map);
        }
        if (X < map[Y].Length - 1 && currentDirection == Direction.RIGHT && map[Y][X + 1] == Convert.ToChar("#"))
        {
            currentDirection = Direction.DOWN;
            CheckForObstacle(map);
        }
        if (Y < map.Count - 1 && currentDirection == Direction.DOWN && map[Y + 1][X] == Convert.ToChar("#"))
        {
            currentDirection = Direction.LEFT;
            CheckForObstacle(map);
        }
        if (X > 0 && currentDirection == Direction.LEFT && map[Y][X - 1] == Convert.ToChar("#"))
        {
            currentDirection = Direction.UP;
            CheckForObstacle(map);
        }
    }

    private static void MarkPositionAsVisited(ref List<string> map)
    {
        char[] updatedMapRow = map[Y].ToCharArray();

        updatedMapRow[X] = Convert.ToChar("X");

        map[Y] = new string(updatedMapRow);
    }

    private static void CheckIfGuardLeft(ref List<string> map)
    {
        if (
            currentDirection == Direction.RIGHT && X + 1 == map[0].Length ||
            currentDirection == Direction.DOWN && Y + 1 == map.Count ||
            currentDirection == Direction.LEFT && X - 1 < 0 ||
            currentDirection == Direction.UP && Y - 1 < 0
        )
        {
            MarkPositionAsVisited(ref map);
            hasGuardLeft = true;
        }
    }

    private static void MoveGuard(ref List<string> map, bool isFirstPath)
    {
        CheckForObstacle(map);
        CheckIfGuardLeft(ref map);

        MarkPositionAsVisited(ref map);
        if (isFirstPath)
        {
            originalPath.Add($"{X}-{Y}");
        }

        if (!hasGuardLeft)
        {
            if (currentDirection == Direction.UP) Y--;
            if (currentDirection == Direction.RIGHT) X++;
            if (currentDirection == Direction.DOWN) Y++;
            if (currentDirection == Direction.LEFT) X--;
        }
    }

    private static int CalculateVisitedPositions(List<string> map)
    {
        int accumulator = 0;

        for (int x = 0; x < map[0].Length; x++)
        {
            for (int y = 0; y < map.Count; y++)
            {
                if (map[y][x].ToString() == "X")
                {
                    accumulator++;
                }
            }
        }

        return accumulator;
    }

    private static void PlaceObstacleAt(int x, int y, ref List<string> map)
    {
        char[] updatedMapRow = map[y].ToCharArray();

        updatedMapRow[x] = Convert.ToChar("#");

        map[y] = new string(updatedMapRow);
    }

    private static bool IsGuardStuckInLoop(ref List<string> pathRecord)
    {
        return pathRecord.Contains($"{X}-{Y}-{currentDirection}");
    }
}