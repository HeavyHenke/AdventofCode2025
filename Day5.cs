namespace AdventofCode2025;

public class Day5
{
    public object Part1()
    {
        var lines = File.ReadAllLines("Day5.txt");
        int lineIx;
        var ranges = new List<(long, long)>();
        for (lineIx = 0; lineIx < lines.Length && lines[lineIx] != ""; lineIx++)
        {
            var range = lines[lineIx].Split('-');
            ranges.Add((long.Parse(range[0]), long.Parse(range[1])));
        }

        lineIx++;
        var fresh = 0;
        while (lineIx < lines.Length)
        {
            var ingredient = long.Parse(lines[lineIx++]);
            if (ranges.Any(r => r.Item1 <= ingredient && ingredient <= r.Item2))
                fresh++;
        }

        return fresh;
    }

    public object Part2()
    {
        var lines = File.ReadAllLines("Day5.txt");
        int lineIx;
        var ranges = new List<(long, long)>();
        for (lineIx = 0; lineIx < lines.Length && lines[lineIx] != ""; lineIx++)
        {
            var range = lines[lineIx].Split('-');
            ranges.Add((long.Parse(range[0]), long.Parse(range[1])));
        }

        bool changed = true;
        while (changed)
        {
            changed = false;
            for (int i = 0; i < ranges.Count; i++)
            {
                for (int r = i + 1; r < ranges.Count; r++)
                {
                    var merged = Merge(ranges[i], ranges[r]);
                    if (merged != null)
                    {
                        ranges[i] = merged.Value;
                        ranges.RemoveAt(r);
                        changed = true;
                    }
                }
            }
        }

        return ranges.Sum(r => r.Item2 - r.Item1 + 1);
    }

    private static (long start, long stop)? Merge((long start, long stop) a, (long start, long stop) b)
    {
        var maxStart = Math.Max(a.start, b.start);
        var minStop = Math.Min(a.stop, b.stop);

        if (maxStart <= minStop)
            return (Math.Min(a.start, b.start), Math.Max(a.stop, b.stop));

        return null;
    }
}