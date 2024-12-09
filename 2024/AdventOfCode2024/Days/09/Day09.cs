namespace AdventOfCode2024.Days._09;

public class Day09 : ADay
{
    protected override Part Execute => Part.Both;

    protected override string Part1()
    {
        var result = ParseInput();

        for (var i = result.Count - 1; i > 0; i--)
        {
            var index = result.IndexOf(".");

            if (index > i) break;

            (result[i], result[index]) = (result[index], result[i]);
        }

        return CheckSum(result, Part.One).ToString();
    }

    protected override string Part2()
    {
        var result = ParseInput();

        for (var i = result.Count - 1; i > 0; i--)
        {
            if (result[i] == ".") continue;

            var index = result.IndexOf(result[i]);
            var toMove = result[index..(i + 1)];

            var spaceIndex = -1;

            for (var j = 0; j < index; j++)
            {
                if (result[j] != ".") continue;

                var space = 0;
                var numHit = false;
                while (!numHit && j + space < result.Count)
                {
                    if (result[j + space] == ".") space++;
                    else numHit = true;
                }

                if (space >= toMove.Count)
                {
                    spaceIndex = j;
                    break;
                }

                j += space;
            }

            if (spaceIndex != -1)
            {
                var empty = result[spaceIndex..(spaceIndex + toMove.Count)];
                result.RemoveRange(spaceIndex, toMove.Count);
                result.InsertRange(spaceIndex, toMove);
                result.RemoveRange(index, toMove.Count);
                result.InsertRange(index, empty);
            }
            else
            {
                i = index;
            }
        }

        return CheckSum(result, Part.Two).ToString();
    }

    private List<string> ParseInput()
    {
        List<string> result = [];
        var instructions = Input[0].ToCharArray().Select(x => x.ToString()).Select(int.Parse)
            .ToList();
        var isFree = false;
        var count = 0;
        foreach (var instruction in instructions)
        {
            for (var i = 0; i < instruction; i++)
            {
                result.Add(isFree ? "." : count.ToString());
            }

            if (!isFree) count++;
            isFree = !isFree;
        }

        return result;
    }

    private static long CheckSum(List<string> input, Part part)
    {
        var checksum = 0L;

        for (var i = 0; i < input.Count; i++)
        {
            if (part == Part.One && input[i] == ".") break;

            if (input[i] != ".")
                checksum += long.Parse(input[i]) * i;
        }

        return checksum;
    }
}