namespace AdventOfCode2024.Days._02;

public class Day02 : ADay
{
    protected override Part Execute => Part.Both;

    protected override string Part1()
    {
        var lines =
            Input.Select(line => line.Split(" ").Select(int.Parse).ToList());

        return lines.Select(CheckSafe).Count(isSafe => isSafe).ToString();
    }

    protected override string Part2()
    {
        var safe = 0;
        foreach (var line in Input)
        {
            var numbers = line.Split(" ").Select(int.Parse).ToList();

            for (var x = 0; x < numbers.Count; x++)
            {
                var test = new List<int>(numbers);
                test.RemoveAt(x);

                if (!CheckSafe(test)) continue;
                
                safe++;
                break;
            }
        }
        return safe.ToString();
    }

    private static bool CheckSafe(List<int> numbers)
    {
        if (!numbers.SequenceEqual(numbers.Order()) &&
            !numbers.SequenceEqual(numbers.OrderDescending()))
            return false;

        for (var i = 0; i < numbers.Count - 1; i++)
        {
            if (Math.Abs(numbers[i] - numbers[i + 1]) is >= 1 and <= 3)
                continue;

            return false;
        }

        return true;
    }
}