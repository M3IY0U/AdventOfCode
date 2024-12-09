using System.Text.RegularExpressions;

namespace AdventOfCode2024.Days._03;

public partial class Day03 : ADay
{
    protected override Part Execute => Part.Both;

    protected override string Part1()
    {
        var regex = Part1Regex();
        var matches = regex.Matches(Input[0]).Cast<Match>();
        var sum = 0;
        
        foreach (var match in matches)
        {
            var vals = match.Groups[1].Value;
            var comma = vals.IndexOf(',');
            sum += int.Parse(vals[..comma]) * int.Parse(vals[(comma + 1)..]);
        }
        
        return sum.ToString();
    }

    protected override string Part2()
    {
        var regex = Part2Regex();
        var matches = regex.Matches(Input[0]).Cast<Match>();
        var total = 0;
        var enabled = true;
        
        foreach (var match in matches)
        {
            switch (match.Value)
            {
                case "do()":
                    enabled = true;
                    continue;
                case "don't()":
                    enabled = false;
                    continue;
            }

            if (!enabled) continue;
            
            var vals = match.Groups[1].Value;
            var comma = vals.IndexOf(',');
            total += int.Parse(vals[..comma]) * int.Parse(vals[(comma + 1)..]);
        }

        return total.ToString();
    }

    [GeneratedRegex(@"mul\((\d{1,3},\d{1,3})\)", RegexOptions.Compiled)]
    private static partial Regex Part1Regex();

    [GeneratedRegex(@"do\(\)|don't\(\)|mul\((\d{1,3},\d{1,3})\)", RegexOptions.Compiled)]
    private static partial Regex Part2Regex();
}