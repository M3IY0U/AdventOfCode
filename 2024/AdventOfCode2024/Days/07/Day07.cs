namespace AdventOfCode2024.Days._07;

public class Day07 : ADay
{
    private static readonly List<string> Operators = ["*", "+"];

    protected override Part Execute => Part.Both;

    protected override string Part1()
    {
        var lines = ParseLines();
        var total = lines.Where(x => x.Check()).Select(line => line.Result);
        return total.Sum().ToString();
    }

    protected override string Part2()
    {
        Operators.Add("|");

        var lines = ParseLines();
        var total = lines.Where(x => x.Check()).Select(line => line.Result);
        return total.Sum().ToString();
    }

    private List<Line> ParseLines()
    {
        var lines = new List<Line>();
        foreach (var line in Input)
        {
            var x = line.Split(": ");
            lines.Add(new Line(long.Parse(x[0]), x[1].Split(" ").Select(long.Parse).ToArray()));
        }

        return lines;
    }


    private class Line(long result, long[] operands)
    {
        public long Result { get; } = result;

        private readonly IEnumerable<string[]> _operatorCombinations =
            GetCombinations(Operators, operands.Length - 1);

        public bool Check()
        {
            var expressions = _operatorCombinations
                .Select(combination => Combine(operands, combination));

            return expressions.Any(x => EvaluateExpression(x) == Result);
        }

        private static long EvaluateExpression(string expression)
        {
            var tokens = expression.Split(' ');
            var result = long.Parse(tokens[0]);

            for (var i = 1; i < tokens.Length; i += 2)
            {
                var op = tokens[i];
                var num = int.Parse(tokens[i + 1]);

                switch (op)
                {
                    case "+":
                        result += num;
                        break;
                    case "*":
                    {
                        result *= num;
                        break;
                    }
                    case "|":
                        result = long.Parse($"{result}{num}");
                        break;
                }
            }

            return result;
        }

        private static IEnumerable<string[]> GetCombinations(List<string> operators, int length)
        {
            if (length == 0) return [[]];
            return GetCombinations(operators, length - 1)
                .SelectMany(_ => operators, (t1, t2) => t1.Concat([t2]).ToArray());
        }

        private static string Combine(long[] numbers, string[] operators)
        {
            var result = numbers[0].ToString();
            for (var i = 0; i < operators.Length; i++)
            {
                result += $" {operators[i]} {numbers[i + 1]}";
            }

            return result;
        }
    }
}