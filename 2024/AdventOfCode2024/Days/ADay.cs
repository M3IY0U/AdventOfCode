using System.Diagnostics;

namespace AdventOfCode2024.Days;

public abstract class ADay
{
    protected ADay() => Input = File.ReadAllLines($"./Days/{GetType().Name[^2..]}/input.txt");
    protected abstract int Part1();
    protected abstract int Part2();
    protected readonly string[] Input;
    protected virtual Part Execute => Part.One;

    public void Run()
    {
        Console.WriteLine($"Day {GetType().Name[^2..]}");
        var sw = new Stopwatch();

        switch (Execute)
        {
            case Part.One:
            {
                sw.Start();
                var p1 = Part1();
                sw.Stop();
                Console.WriteLine(
                    $"Part 1: {p1} | {sw.ElapsedMilliseconds} ms");
                sw.Reset();
                break;
            }
            case Part.Two:
            {
                sw.Start();
                var p2 = Part2();
                sw.Stop();
                Console.WriteLine(
                    $"Part 2: {p2} | {sw.Elapsed.TotalMilliseconds} ms");
                break;
            }
            case Part.Both:
            {
                sw.Start();
                var p1 = Part1();
                sw.Stop();
                Console.WriteLine(
                    $"Part 1: {p1} | {sw.Elapsed.TotalMilliseconds} ms");

                sw.Restart();
                var p2 = Part2();
                sw.Stop();
                Console.WriteLine(
                    $"Part 2: {p2} | {sw.Elapsed.TotalMilliseconds} ms");
                break;
            }
            default:
                throw new ArgumentOutOfRangeException();
        }

    }
}