using System.Reflection;
using AdventOfCode2024.Days;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode2024;

[MemoryDiagnoser]
public class AocBenchmark
{
    private ADay _currentDay = null!;
    private MethodInfo _part1Method = null!;
    private MethodInfo _part2Method = null!;

    [Params(5)]
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public int Day { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        var dayName = $"Day{Day:00}";
        var dayType = Assembly.GetExecutingAssembly()
            .GetTypes()
            .FirstOrDefault(t =>
                t.Name == dayName && typeof(ADay).IsAssignableFrom(t));

        if (dayType == null)
            throw new($"Day `{dayName}` not found");

        _currentDay = (ADay)Activator.CreateInstance(dayType)!;

        _part1Method = dayType.GetMethod("Part1",
                           BindingFlags.NonPublic | BindingFlags.Instance)
                       ?? throw new InvalidOperationException(
                           "Part1 method not found.");
        _part2Method = dayType.GetMethod("Part2",
                           BindingFlags.NonPublic | BindingFlags.Instance)
                       ?? throw new InvalidOperationException(
                           "Part2 method not found.");
    }

    [Benchmark(Baseline = true)]
    public object Part1() => _part1Method.Invoke(_currentDay, null)!;

    [Benchmark]
    public object Part2() => _part2Method.Invoke(_currentDay, null)!;
}