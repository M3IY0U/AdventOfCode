namespace AdventOfCode2024.Days._05;

public class Day05 : ADay
{
    protected override Part Execute => Part.Both;

    private readonly HashSet<int>[] _updates;
    private readonly List<(int, int)> _constraints;

    public Day05()
    {
        _updates = Input.SkipWhile(x => x != "").Skip(1)
            .Select(x => x.Split(",").Select(int.Parse).ToHashSet()).ToArray();

        _constraints = Input.TakeWhile(x => x != "")
            .Select(pair =>
            {
                var parts = pair.Split('|');
                return (int.Parse(parts[0]), int.Parse(parts[1]));
            })
            .ToList();
    }

    protected override string Part1()
    {
        var correct = from update in _updates
            let corrected = CorrectOrder(update)
            where corrected.SequenceEqual(update)
            select update.ElementAt(update.Count / 2);

        return correct.Sum().ToString();
    }

    protected override string Part2()
    {
        var correct = from update in _updates
            let corrected = CorrectOrder(update)
            where !corrected.SequenceEqual(update)
            select corrected[corrected.Count / 2];

        return correct.Sum().ToString();
    }

    private List<int> CorrectOrder(HashSet<int> set)
    {
        var inDegree = set.ToDictionary(node => node, _ => 0);
        var adjacencyList =
            set.ToDictionary(node => node, _ => new List<int>());

        foreach (var (from, to) in _constraints)
        {
            if (!set.Contains(from) || !set.Contains(to)) continue;

            adjacencyList[from].Add(to);
            inDegree[to]++;
        }

        // https://en.wikipedia.org/wiki/Topological_sorting#Kahn's_algorithm
        var queue =
            new Queue<int>(set.Where(node => inDegree[node] == 0));
        var sortedList = new List<int>();

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            sortedList.Add(current);

            foreach (var neighbor in adjacencyList[current])
            {
                inDegree[neighbor]--;
                if (inDegree[neighbor] == 0)
                {
                    queue.Enqueue(neighbor);
                }
            }
        }

        return sortedList.Where(set.Contains).ToList();
    }
}