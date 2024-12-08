namespace AdventOfCode2024.Days._06;

public class Day06 : ADay
{
    private readonly char[][] _grid;
    private (int y, int x) _guardPos;
    private Direction _currentDirection;
    private readonly HashSet<(int x, int y)> _visited;

    protected override Part Execute => Part.Both;

    public Day06()
    {
        _visited = [];
        _grid = Input.Select(x => x.ToCharArray()).ToArray();
        _currentDirection = Direction.North;
        for (var i = 0; i < _grid.Length; i++)
        {
            for (var j = 0; j < _grid[i].Length; j++)
            {
                if (_grid[i][j] != '^')
                    continue;

                _guardPos = (i, j);
                break;
            }
        }
    }

    protected override int Part1()
    {
        var originalPos = _guardPos;
        while (_guardPos is { x: >= 0, y: >= 0 } && 
               _guardPos.x < _grid.Length &&
               _guardPos.y < _grid[0].Length)
        {
            Move(true, _grid);
        }

        _guardPos = originalPos;
        return _visited.Count;
    }

    protected override int Part2()
    {
        var originalGuardPos = _guardPos;
        var potentialLocations = 0;
        for (var i = 0; i < _grid.Length; i++)
        {
            for (var j = 0; j < _grid[0].Length; j++)
            {
                if ((j, i) == _guardPos || _grid[j][i] == '#') continue;

                var originalValue = _grid[j][i];
                _grid[j][i] = '#';

                if (IsInLoop(_grid)) potentialLocations++;

                _grid[j][i] = originalValue;
                _guardPos = originalGuardPos;
                _currentDirection = Direction.North;
            }
        }

        return potentialLocations;
    }

    private void Move(bool mark, char[][] grid)
    {
        if (mark)
        {
            _grid[_guardPos.y][_guardPos.x] = 'x';
            _visited.Add((_guardPos.x, _guardPos.y));
        }

        var nextPos = NextPos(_currentDirection, _guardPos);
        if (IsOutOfBounds(nextPos.y, nextPos.x, grid))
        {
            _guardPos = nextPos;
            return;
        }

        if (grid[nextPos.y][nextPos.x] == '#')
        {
            _currentDirection = _currentDirection switch
            {
                Direction.North => Direction.East,
                Direction.South => Direction.West,
                Direction.East => Direction.South,
                Direction.West => Direction.North,
                _ => throw new()
            };
        }
        else _guardPos = nextPos;
    }

    private static bool IsOutOfBounds(int y, int x, char[][] grid) =>
        x < 0 || x >= grid.Length || y < 0 || y >= grid[0].Length;

    private static (int y, int x) NextPos(Direction direction,
        (int y, int x) currentPos)
    {
        return direction switch
        {
            Direction.North => (currentPos.y - 1, currentPos.x),
            Direction.South => (currentPos.y + 1, currentPos.x),
            Direction.East => (currentPos.y, currentPos.x + 1),
            Direction.West => (currentPos.y, currentPos.x - 1),
            _ => throw new ArgumentOutOfRangeException(nameof(direction),
                direction, null)
        };
    }

    private bool IsInLoop(char[][] testGrid)
    {
        var current = 0;
        const int maxSteps = 130 * 130;
        while (!IsOutOfBounds(_guardPos.y, _guardPos.x, testGrid) && current++ < maxSteps)
            Move(false, testGrid);

        return !IsOutOfBounds(_guardPos.y, _guardPos.x, testGrid);
    }
}

internal enum Direction
{
    North,
    South,
    East,
    West
}