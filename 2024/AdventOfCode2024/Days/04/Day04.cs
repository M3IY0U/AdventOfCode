namespace AdventOfCode2024.Days._04;

public class Day04 : ADay
{
    protected override Part Execute => Part.Both;

    private readonly int _rows;
    private readonly int _cols;
    private readonly char[][] _grid;

    private static readonly int[] DirectionsX = [-1, -1, -1, 0, 0, 1, 1, 1];
    private static readonly int[] DirectionsY = [-1, 0, 1, -1, 1, -1, 0, 1];
    private readonly string[] _crossWords = ["MAS", "SAM"];

    private bool IsOutsideGrid(int x, int y) =>
        x < 0 || y < 0 || x >= _cols || y >= _rows;

    public Day04()
    {
        _rows = Input.Length;
        _cols = Input[0].Length;
        _grid = Input.Select(x => x.ToCharArray()).ToArray();
    }

    protected override int Part1()
    {
        var count = 0;

        for (var i = 0; i < _rows; i++)
        for (var j = 0; j < _cols; j++)
        for (var k = 0; k < 8; k++)
            if (CheckWord("XMAS", i, j, DirectionsX[k], DirectionsY[k]))
                count++;

        return count;
    }

    private bool CheckWord(
        string word,
        int x,
        int y,
        int dirX,
        int dirY)
    {
        var index = 0;

        while (true)
        {
            if (index == word.Length) return true;

            if (IsOutsideGrid(x, y) || word[index] != _grid[x][y])
                return false;

            index += 1;
            x += dirX;
            y += dirY;
        }
    }

    protected override int Part2()
    {
        var sum = 0;
        for (var i = 0; i < _rows; i++)
        for (var j = 0; j < _cols; j++)
            if (CheckCross('A', i, j))
                sum++;

        return sum;
    }

    private bool CheckCross(char character, int x, int y)
    {
        if (_grid[x][y] != character)
            return false;

        int[] offsets = [-1, 1];

        if (offsets.Any(offset => IsOutsideGrid(x + offset, y + offset)))
            return false;

        var diag1 = new string([
            _grid[x - 1][y - 1], _grid[x][y], _grid[x + 1][y + 1]
        ]);
        var diag2 = new string([
            _grid[x + 1][y - 1], _grid[x][y], _grid[x - 1][y + 1]
        ]);

        return _crossWords.Contains(diag1) && _crossWords.Contains(diag2);
    }
}