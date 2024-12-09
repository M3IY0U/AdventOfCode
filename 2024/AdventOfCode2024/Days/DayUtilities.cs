namespace AdventOfCode2024.Days;

public abstract partial class ADay
{
    protected static void PrintGrind<T>(T[][] grid)
    {
        var lines = grid.Select(x => string.Join("", x));
        foreach (var line in lines)
        {
            Console.WriteLine(line);
        }
    }

    public static bool IsWithinBounds(int x, int y, int width, int height) =>
        x >= 0 && x < width && y >= 0 && y < height;

    protected static bool IsWithinBounds((int x, int y) point, int width, int height) =>
        point.x >= 0 && point.x < width && point.y >= 0 && point.y < height;
}