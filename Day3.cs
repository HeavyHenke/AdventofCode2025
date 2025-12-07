namespace AdventofCode2025;

public class Day3
{
    public string Part1()
    {
        var banks  =File.ReadAllLines("Day3.txt");
        long sum = 0;

        foreach (var bank in banks)
        {
            var max = bank.SkipLast(1).Max();
            var maxIx = bank.IndexOf(max);
            var max2 = bank.Skip(maxIx + 1).Max();
            var joltage = int.Parse(max + "" + max2);
            sum += joltage;
        }
        
        return sum.ToString();
    }

    public object Part2()
    {
        var banks = File.ReadAllLines("Day3.txt");
        long sum = 0;

        foreach (var bank in banks)
        {
            long joltage = 0;
            var atIx = 0;
            for (int i = 0; i < 12; i++)
            {
                var max = bank.Skip(atIx).SkipLast(11 - i).Max();
                joltage += (max - '0') * (long) Math.Pow(10, 11 - i);

                var maxIx = bank.IndexOf(max, atIx);
                atIx = maxIx + 1;
            }
            sum += joltage;
        }
        
        return sum.ToString();
    }
}