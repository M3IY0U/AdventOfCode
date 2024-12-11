namespace AdventOfCode2024.Days._11;

public class Day11 : ADay
{
    protected override Part Execute => Part.Both;

    private readonly long[] _stones;

    public Day11() => _stones = Input[0].Split().Select(long.Parse).ToArray();

    protected override string Part1() => SimulateStones(25, _stones).ToString();

    protected override string Part2() => SimulateStones(75, _stones).ToString();

    private static long SimulateStones(int number, long[] stones)
    {
        var stoneCounts = new Dictionary<long, long>();
        var newStoneCounts = new Dictionary<long, long>();

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
                if (stone == 0)
                {
                    UpdateDict(newStoneCounts, 1, count);
                }
                else if (SplitStone(stone, out var left, out var right))
                {
                    UpdateDict(newStoneCounts, left, count);
                    UpdateDict(newStoneCounts, right, count);
                }
                else
                {
                    UpdateDict(newStoneCounts, stone * 2024, count);
                }
            }

            stoneCounts = new(newStoneCounts);
            newStoneCounts.Clear();
        }

        return stoneCounts.Values.Sum();
    }

    private static bool SplitStone(long stone, out long left, out long right)
    {
        var digits = (long)Math.Log10(stone) + 1;

        left = 0;
        right = 0;
        if (digits % 2 != 0) return false;

        var tenPow = (long)Math.Pow(10, digits / 2d);

        left = stone / tenPow;
        right = stone % tenPow;

        return true;
    }

    private static void UpdateDict(Dictionary<long, long> dict, long key, long value)
    {
        if (!dict.TryAdd(key, value))
            dict[key] += value;
    }
}