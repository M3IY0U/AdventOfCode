namespace AdventOfCode2024.Days._04;

public class Day04 : ADay
{
    private readonly int _rows;
    private readonly int _cols;
    private readonly char[][] _grid;

    private readonly int[] _directionsX = [-1, -1, -1, 0, 0, 1, 1, 1];
    private readonly int[] _directionsY = [-1, 0, 1, -1, 1, -1, 0, 1];

    public Day04()
    {
        _rows = Input.Length;
        _cols = Input[0].Length;
        _grid = Input.Select(x => x.ToCharArray()).ToArray();
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

            if (x < 0 || y < 0 || x >= _cols || y >= _rows ||
                word[index] != _grid[x][y]) return false;

            index += 1;
            x += dirX;
            y += dirY;
        }
    }

    protected override int Part1()
    {
        var count = 0;

        for (var i = 0; i < _rows; i++)
        for (var j = 0; j < _cols; j++)
        for (var k = 0; k < 8; k++)
            if (CheckWord("XMAS", i, j, _directionsX[k], _directionsY[k]))
                count++;

        return count;
    }

    protected override int Part2()
    {
        throw new NotImplementedException();
    }
}