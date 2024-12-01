using System.Diagnostics;

namespace AdventOfCode2024.Days;

public abstract class ADay
{
    protected ADay() => Input = File.ReadAllLines($"./Days/{GetType().Name[^2..]}/input.txt");
    protected abstract int Part1();
    protected abstract int Part2();
    protected readonly string[] Input;

    public void Run()
    {
        Console.WriteLine($"Day {GetType().Name[^2..]}");
        var sw = new Stopwatch();

        sw.Start();
        var p1 = Part1();
        sw.Stop();
        Console.WriteLine($"Part 1: {p1} | {sw.ElapsedMilliseconds} ms");
        
        sw.Restart();
        var p2 = Part2();
        sw.Stop();
        Console.WriteLine($"Part 2: {p2} | {sw.ElapsedMilliseconds} ms");
    }
}