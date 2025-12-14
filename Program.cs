// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using AdventofCode2025;

var sw = Stopwatch.StartNew();

var result = new Day9().Part2();

sw.Stop();

Console.WriteLine($"Done, it took {sw.Elapsed}, answer:");
Console.WriteLine(result);

Console.WriteLine("");
// Console.WriteLine("Press C to copy to clipboard");
// var key = Console.ReadKey();
// if(key.Key == ConsoleKey.C)
//     System.Windows.Clipboard.SetText(result);
Console.WriteLine();
Console.WriteLine("Exited");