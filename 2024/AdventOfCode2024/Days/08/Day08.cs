namespace AdventOfCode2024.Days._08;

public class Day08 : ADay
{
    private readonly List<Antenna> _antennae = [];
    private readonly int _width;
    private readonly int _height;

    private record Antenna(int X, int Y, char Freq);

    protected override Part Execute => Part.Both;

    public Day08()
    {
        _width = Input.Length;
        _height = Input[0].Length;
        for (var i = 0; i < Input.Length; i++)
        {
            var line = Input[i];
            for (var j = 0; j < line.Length; j++)
            {
                if (line[j] != '.')
                    _antennae.Add(new(j, i, line[j]));
            }
        }
    }

    protected override string Part1()
    {
        var nodes = FindAntiNodes(false);
        return $"{nodes.Distinct().Count()}";
    }

    protected override string Part2()
    {
        var nodes = FindAntiNodes(true);
        _antennae.ForEach(a => nodes.Add((a.X, a.Y)));
        return $"{nodes.Distinct().Count()}";
    }

    private List<(int x, int y)> FindAntiNodes(bool part2)
    {
        var result = new List<(int x, int y)>();
        foreach (var freqGroup in _antennae.GroupBy(x => x.Freq))
        {
            for (var i = 0; i < freqGroup.Count(); i++)
            {
                for (var j = 0; j < freqGroup.Count(); j++)
                {
                    var a = freqGroup.ElementAt(i);
                    var b = freqGroup.ElementAt(j);

                    if (a == b) continue;

                    (int x, int y) dist = (a.X - b.X, a.Y - b.Y);
                    (int x, int y) location = (a.X - -dist.x, a.Y - -dist.y);

                    if (!part2)
                    {
                        if (IsWithinBounds(location, _width, _height))
                            result.Add(location);
                    }
                    else
                    {
                        while (IsWithinBounds(location, _width, _height))
                        {
                            result.Add(location);
                            location = (location.x - -dist.x, location.y - -dist.y);
                        }
                    }
                }
            }
        }

        return result;
    }
}