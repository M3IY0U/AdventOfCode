namespace AdventOfCode2024.Days._10;

public class Day10 : ADay
{
    private readonly int[][] _grid;
    private readonly List<TrailHead> _trailHeads = [];
    private readonly int _width;
    private readonly int _height;
    private bool _rating;

    protected override Part Execute => Part.Both;

    private class TrailHead
    {
        public int Score { get; set; }
        public (int x, int y) Start;
        public readonly HashSet<(int x, int y)> Ends = [];
    }

    public Day10()
    {
        var temp = Input.Select(line =>
            line.ToCharArray().Select(x =>
                int.Parse(x.ToString())).ToArray()).ToArray();

        _grid = temp[0]
            .Select((_, colIndex) => temp.Select(row => row[colIndex]).ToArray())
            .ToArray();

        _width = _grid.Length;
        _height = _grid[0].Length;

        for (var i = 0; i < Input.Length; i++)
        {
            var line = Input[i];
            for (var j = 0; j < line.Length; j++)
            {
                if (line[j] == '0')
                    _trailHeads.Add(new()
                    {
                        Score = 0,
                        Start = (j, i)
                    });
            }
        }
    }

    protected override string Part1()
    {
        _rating = false;
        return CalculateScores().ToString();
    }

    protected override string Part2()
    {
        _rating = true;
        _trailHeads.ForEach(x => x.Score = 0);
        return CalculateScores().ToString();
    }

    private int CalculateScores()
    {
        var tasks = _trailHeads.Select((trailHead, i) =>
            Task.Run(() => WalkTrail(0, trailHead.Start, i, [])));
        
        Task.WaitAll(tasks);
        return _trailHeads.Sum(t => t.Score);
    }

    private void WalkTrail(int current, (int x, int y) pos, int headIndex,
        HashSet<(int x, int y)> visited)
    {
        if (!IsWithinBounds(pos, _width, _height)) // out of bounds
            return;

        if (visited.Contains(pos)) // already visited
            return;

        if (_grid[pos.x][pos.y] != current) // no elevation
            return;

        if (_grid[pos.x][pos.y] == 9)
        {
            if (_rating)
            {
                _trailHeads[headIndex].Score += 1;
                return;
            }

            if (!_trailHeads[headIndex].Ends.Add(pos)) return;
            _trailHeads[headIndex].Score += 1;
            return;
        }

        if (_rating) visited.Add(pos);

        WalkTrail(current + 1, (pos.x, pos.y - 1), headIndex, [..visited]);
        WalkTrail(current + 1, (pos.x, pos.y + 1), headIndex, [..visited]);
        WalkTrail(current + 1, (pos.x + 1, pos.y), headIndex, [..visited]);
        WalkTrail(current + 1, (pos.x - 1, pos.y), headIndex, [..visited]);
    }
}