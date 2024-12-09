namespace AdventOfCode2024.Days._01;

public class Day01 : ADay
{
    private readonly (List<int>, List<int>) _parsed;
    protected override Part Execute => Part.Both;
    public Day01() => _parsed = ParseInput();

    private (List<int>, List<int>) ParseInput()
    {
        var list1 = new List<int>();
        var list2 = new List<int>();

        foreach (var line in Input)
        {
            var numbers = line.Split("   ");
            list1.Add(int.Parse(numbers[0]));
            list2.Add(int.Parse(numbers[1]));
        }

        list1.Sort();
        list2.Sort();

        return (list1, list2);
    }

    protected override string Part1()
    {
        var (list1, list2) = _parsed;

        var differences =
            list1.Select((t, i) => Math.Abs(t - list2[i])).ToList();

        return differences.Sum().ToString();
    }

    protected override string Part2()
    {
        var (list1, list2) = _parsed;

        var similarities =
            list1.Select(t => t * list2.Count(l => l == t)).ToList();

        return similarities.Sum().ToString();
    }
}