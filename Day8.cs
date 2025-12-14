namespace AdventofCode2025;

public class Day8
{
    public object Part1()
    {
        var fileContent = File.ReadAllLines("Day8.txt");
        var coords = fileContent.Select(Parse).ToArray();

        // Calc distances
        var distances = new List<((int x, int y, int z) a, (int x, int y, int z)b, long dist)>();
        for (var a = 0; a < coords.Length; a++)
        for (var b = a + 1; b < coords.Length; b++)
        {
            var dist = GetDistanceSquared(coords[a], coords[b]);
            distances.Add((coords[a], coords[b], dist));
        }

        distances.Sort((a, b) => a.dist.CompareTo(b.dist));
        Dictionary<(int x, int y, int z), int> circuits = new();
        HashSet<((int x, int y, int z) a, (int x, int y, int z)b)> connections = new();
        
        for (int con = 0; con < 1_000; con++)
        {
            Console.WriteLine($"Iteration {con}");

            var nextCircuit = distances
                .First(d => !connections.Contains((d.a, d.b)));
            
            var shortestA = nextCircuit.a;
            var shortestB = nextCircuit.b;
            
            var isAinCir = circuits.TryGetValue(shortestA, out var circA);
            var isBinCir = circuits.TryGetValue(shortestB, out var circB);

            if (isAinCir && isBinCir)
            {
                // Move circB to circA
                if (circA != circB)
                {
                    var fromB = circuits.Where(c => c.Value == circB).ToArray();
                    foreach (var p in fromB)
                        circuits[p.Key] = circA;
                }
            }
            else if (isAinCir)
            {
                circuits.Add(shortestB, circA);
            }
            else if (isBinCir)
            {
                circuits.Add(shortestA, circB);
            }
            else
            {
                circA = circuits.Count + 1;
                circuits.Add(shortestA, circA);
                circuits.Add(shortestB, circA);
            }
            
            connections.Add((shortestA, shortestB));
        }

        var circuitSizes = circuits
            .GroupBy(c => c.Value)
            .Select(g => g.Count())
            .OrderByDescending(q => q)
            .Take(3);
        
        // 12720 to low
        var prod = circuitSizes.Aggregate(1L, (a, b) => a * b);
        return prod;
    }

    public object Part2()
    {
        var fileContent = File.ReadAllLines("Day8.txt");
        var coords = fileContent.Select(Parse).ToArray();

        // Calc distances
        var distances = new List<((int x, int y, int z) a, (int x, int y, int z)b, long dist)>();
        for (var a = 0; a < coords.Length; a++)
        for (var b = a + 1; b < coords.Length; b++)
        {
            var dist = GetDistanceSquared(coords[a], coords[b]);
            distances.Add((coords[a], coords[b], dist));
        }

        distances.Sort((a, b) => a.dist.CompareTo(b.dist));
        var circuits = new Dictionary<(int x, int y, int z), int>();
        int numCircuits = coords.Length;

        int lastx1 = 0;
        int lastx2 = 0;
        int distIx = 0;

        while (numCircuits > 1)
        {
            var nextCircuit = distances[distIx++];

            var shortestA = nextCircuit.a;
            var shortestB = nextCircuit.b;

            var isAinCir = circuits.TryGetValue(shortestA, out var circA);
            var isBinCir = circuits.TryGetValue(shortestB, out var circB);

            lastx1 = shortestA.x;
            lastx2 = shortestB.x;

            if (isAinCir && isBinCir)
            {
                // Move circB to circA
                if (circA != circB)
                {
                    var fromB = circuits.Where(c => c.Value == circB).ToArray();
                    foreach (var p in fromB)
                        circuits[p.Key] = circA;
                    numCircuits--;
                }
            }
            else if (isAinCir)
            {
                circuits.Add(shortestB, circA);
                numCircuits--;
            }
            else if (isBinCir)
            {
                circuits.Add(shortestA, circB);
                numCircuits--;
            }
            else
            {
                circA = circuits.Count + 1;
                circuits.Add(shortestA, circA);
                circuits.Add(shortestB, circA);
                numCircuits--;
            }
        }

        return (long)lastx1 * lastx2;
    }

    
    private static long GetDistanceSquared((int x, int y, int z) p1, (int x, int y, int z) p2)
    {
        var dx = (long)p1.x - p2.x;
        var dy = (long)p1.y - p2.y;
        var dz = (long)p1.z - p2.z;
        var dist = dx * dx + dy * dy + dz * dz;
        return dist;
    }

    private static (int x, int y, int z) Parse(string line)
    {
        var s = line.Split(',');
        return (int.Parse(s[0]), int.Parse(s[1]), int.Parse(s[2]));
    }
}