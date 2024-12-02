namespace Advent;
public class Day1
{
    public async static Task Solve()
    {
        string responseBody = await AdventClient.GetDataForDay(1);

        string[] lines = responseBody.Split("\n");
        List<string[]> items = lines.Select((line) => line.Split("   ")).SkipLast(1).ToList();

        List<int> list1 = [];
        List<int> list2 = [];

        foreach (var item in items)
        {
            list1.Add(Convert.ToInt32(item[0]));
            list2.Add(Convert.ToInt32(item[1]));
        }

        /* PART 1 */
        list1.Sort();
        list2.Sort();

        var accumulator = 0;

        for (var i = 0; i < list1.Count; i++)
        {
            accumulator += Math.Abs(list1[i] - list2[i]);
        }

        Console.WriteLine(accumulator); // 2066446

        /* PART 2 */

        int similarityScore = 0;

        foreach (var list1Item in list1)
        {
            int timesItemAppearsInList2 = 0;

            foreach (var list2Item in list2)
            {
                if (list1Item == list2Item)
                {
                    timesItemAppearsInList2++;
                }
            }

            similarityScore += list1Item * timesItemAppearsInList2;
        }

        Console.WriteLine(similarityScore); // 24931009
    }

}

