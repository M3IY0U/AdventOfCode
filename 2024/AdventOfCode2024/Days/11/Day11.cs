namespace AdventOfCode2024.Days._11;

public class Day11 : ADay
{
    protected override Part Execute => Part.Both;

    private string[] Stones => Input[0].Split();

    protected override string Part1() => SimulateStones(25, Stones).ToString();

    protected override string Part2() => SimulateStones(75, Stones).ToString();

    private static long SimulateStones(int number, string[] stones)
    {
        var stoneCounts = new Dictionary<string, long>();
        var newStoneCounts = new Dictionary<string, long>();

        foreach (var stone in stones)
        {
            if (stoneCounts.TryGetValue(stone, out var value))
                stoneCounts[stone] = ++value;
            else
                stoneCounts[stone] = 1;
        }

        for (var i = 0; i < number; i++)
        {
            foreach (var (stone, count) in stoneCounts)
            {
                if (stone == "0")
                {
                    UpdateDict(newStoneCounts, "1", count);
                }
                else if (stone.Length % 2 == 0)
                {
                    var mid = stone.Length / 2;
                    var left = stone[..mid].TrimStart('0');
                    var right = stone[mid..].TrimStart('0');

                    if (string.IsNullOrEmpty(left)) left = "0";
                    if (string.IsNullOrEmpty(right)) right = "0";

                    UpdateDict(newStoneCounts, left, count);
                    UpdateDict(newStoneCounts, right, count);
                }
                else
                {
                    var newNumber = long.Parse(stone) * 2024;
                    UpdateDict(newStoneCounts, newNumber.ToString(), count);
                }
            }

            stoneCounts = new(newStoneCounts);
            newStoneCounts.Clear();
        }

        return stoneCounts.Values.Sum();
    }

    private static void UpdateDict(Dictionary<string, long> dict, string key, long value)
    {
        if (!dict.TryAdd(key, value))
            dict[key] += value;
    }
}