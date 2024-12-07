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

    public async static Task Solve()
    {
        string responseBody = await AdventClient.GetDataForDay(6);
        List<string> map = responseBody.Split("\n").SkipLast(1).ToList();

        GetInitialCoordinates(map);

        while (!hasGuardLeft)
        {
            MoveGuard(ref map);
        }

        Console.WriteLine(CalculateVisitedPositions(map));
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

    private static void MoveGuard(ref List<string> map)
    {
        CheckForObstacle(map);
        CheckIfGuardLeft(ref map);

        if (!hasGuardLeft)
        {
            MarkPositionAsVisited(ref map);

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
}