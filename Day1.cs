namespace AdventofCode2025;

public class Day1
{
    public string Part1()
    {
        var file = File.ReadAllLines("Day1.txt");

        int pos = 50;
        int num0 = 0;
        
        foreach (var command in file)
        {
            var dir = command[0] == 'L' ? -1 : 1;
            var steps = int.Parse(command[1..]);
            
            pos += dir * steps;

            while (pos > 99)
                pos -= 100;
            while (pos < 0)
                pos += 100;
            
            if (pos == 0)
                num0++;
        }
        
        return num0.ToString();
    }
    
    public string Part2()
    {
        var file = File.ReadAllLines("Day1.txt");

        int pos = 50;
        int num0 = 0;
        
        foreach (var command in file)
        {
            var dir = command[0] == 'L' ? -1 : 1;
            var steps = int.Parse(command[1..]);
            
            var fullRots = steps / 100;
            var rest = steps % 100;

            num0 += fullRots;
            if (rest > 0)
            {
                bool was0 = pos == 0;
                pos += dir * rest;
                if (pos > 99)
                {
                    pos -= 100;
                    if(pos != 0 && ! was0)
                        num0++;
                }
                else if (pos < 0)
                {
                    pos += 100;
                    if(pos != 0 && !was0)
                        num0++;
                }
                if (pos == 0)
                {
                    num0++;
                }
            }
        }
        
        return num0.ToString();
    }
}