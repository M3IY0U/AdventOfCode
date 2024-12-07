namespace AdventOfCode2024.Days._06;

public class Day06 : ADay
{
    private readonly char[][] _grid;
    private (int y, int x) _guardPos;
    private Direction _currentDirection;
    private HashSet<(int x, int y)> _visited;

    protected override Part Execute => Part.Two;

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

    private void PrintGrid()
    {
        var grid = _grid.Select(x => string.Join("", x));
        foreach (var row in grid)
        {
            Console.WriteLine(row);
        }

        Console.WriteLine("------------------");
    }

    protected override int Part1()
    {
        while (_guardPos is { x: >= 0, y: >= 0 } &&
               _guardPos.x < _grid.Length && _guardPos.y < _grid[0].Length)
        {
            _currentDirection = NextDirection(_grid);
            Move(true);
        }

        return _visited.Count;
    }

    private void Move(bool mark)
    {
        if (mark)
        {
            _grid[_guardPos.y][_guardPos.x] = 'x';
            _visited.Add((_guardPos.x, _guardPos.y));
        }

        _guardPos = _currentDirection switch
        {
            Direction.North => (_guardPos.y - 1, _guardPos.x),
            Direction.South => (_guardPos.y + 1, _guardPos.x),
            Direction.East => (_guardPos.y, _guardPos.x + 1),
            Direction.West => (_guardPos.y, _guardPos.x - 1),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private Direction NextDirection(char[][] grid)
    {
        switch (_currentDirection)
        {
            case Direction.North:
                if (_guardPos.y == 0) return Direction.North;
                if (grid[_guardPos.y - 1][_guardPos.x] == '#')
                    return Direction.East;
                break;
            case Direction.South:
                if (_guardPos.y == _grid.Length - 1) return Direction.South;
                if (grid[_guardPos.y + 1][_guardPos.x] == '#')
                    return Direction.West;
                break;
            case Direction.East:
                if (_guardPos.x == _grid[0].Length - 1) return Direction.East;
                if (grid[_guardPos.y][_guardPos.x + 1] == '#')
                    return Direction.South;
                break;
            case Direction.West:
                if (_guardPos.x == 0) return Direction.West;
                if (grid[_guardPos.y][_guardPos.x - 1] == '#')
                    return Direction.North;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return _currentDirection;
    }


    protected override int Part2()
    {
        var originalGuardPos = _guardPos;
        var potentialLocations = 0;
        for (int i = 0; i < _grid.Length; i++)
        {
            for (int j = 0; j < _grid[0].Length; j++)
            {
                if ((j, i) == _guardPos) continue;

                char[][] testGrid = _grid.Select(a => a.ToArray()).ToArray();
                testGrid[j][i] = '#';

                if (TestGrid(testGrid))
                {
                    potentialLocations++;
                }

                _guardPos = originalGuardPos;
                _currentDirection = Direction.North;
            }
        }

        return potentialLocations;
    }

    private bool TestGrid(char[][] testGrid)
    {
        for (int i = 0; i < 130*130*130; i++)
        {
            _currentDirection = NextDirection(testGrid);
            Move(false);

            if (_guardPos.x < 0 || _guardPos.y < 0 ||
                _guardPos.x >= testGrid.Length ||
                _guardPos.y >= testGrid[0].Length)
                return false;
        }

        return true;
    }
}

internal enum Direction
{
    North,
    South,
    East,
    West
}