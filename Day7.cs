namespace AdventofCode2025;

public class Day7
{
    public object Part1()
    {
        var rows = File.ReadAllLines("Day7.txt");
        var startPos = rows[0].IndexOf('S');

        var beams = new HashSet<int> { startPos };
        int numSplits = 0;
        foreach (var row in rows.Skip(1))
        {
            var next = new HashSet<int>();
            foreach (var beam in beams)
            {
                if (row[beam] == '^')
                {
                    next.Add(beam + 1);
                    next.Add(beam - 1);
                    numSplits++;
                }
                else
                {
                    next.Add(beam);
                }
            }

            beams = next;
        }

        return numSplits;
    }

    public object Part2()
    {
        var rows = File.ReadAllLines("Day7.txt");
        var startPos = rows[0].IndexOf('S');

        long numTimelines = NumTimelines(rows, 1, startPos);

        return numTimelines;
    }

    private readonly Dictionary<(int row, int beamIx), long> _cache = new();
    
    private long NumTimelines(string[] rows, int rowIx, int beamIx)
    {
        while (rowIx < rows.Length)
        {
            if (rows[rowIx][beamIx] == '^')
            {
                if (_cache.TryGetValue((rowIx + 1, beamIx), out var cached))
                    return cached;

                var numTimelines = NumTimelines(rows, rowIx + 1, beamIx + 1) + NumTimelines(rows, rowIx + 1, beamIx - 1);
                _cache.Add((rowIx + 1, beamIx), numTimelines);
                return numTimelines;
            }
            rowIx++;
        }

        return 1;
    }
}