using AdventOfCode2024;
using AdventOfCode2024.Days;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

var days = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
    .Where(x => typeof(ADay).IsAssignableFrom(x) &&
                x is { IsAbstract: false, IsClass: true }).ToList();


void CurrentDay()
{
    var current = DateTime.Now.ToString("dd");
    var day = days.Find(x =>
        x.FullName != null && x.FullName.EndsWith(current));
    var d = (ADay)Activator.CreateInstance(
        day ?? throw new("Day not found"))!;
    d.Run();
}

#if DEBUG
CurrentDay();
return;

#endif

Console.WriteLine(
    "`B` for benchmarking or anything else for normal execution: ");
var input = Console.ReadLine();

if (input?.ToLower() == "b")
{
    var config = ManualConfig.CreateMinimumViable()
        .WithOption(ConfigOptions.DisableLogFile, true);

    BenchmarkRunner.Run<AocBenchmark>(config);
    return;
}

Console.WriteLine(
    "Empty/Enter for current day or a number for specific day or `all` for all days");
input = Console.ReadLine();

switch (input)
{
    case "":
    {
        CurrentDay();
        break;
    }
    case "all":
    {
        days.ForEach(day =>
        {
            var d = (ADay)Activator.CreateInstance(day)!;
            d.Run();
        });
        break;
    }
    default:
    {
        if (int.TryParse(input, out var dayInt))
        {
            var day = days.Find(x =>
                x.FullName != null &&
                x.FullName.EndsWith(dayInt.ToString("00")));
            var d = (ADay)Activator.CreateInstance(
                day ?? throw new("Day not found"))!;
            d.Run();
        }
        else Console.WriteLine("Day could not be parsed");

        break;
    }
}