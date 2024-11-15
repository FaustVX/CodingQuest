// https://codingquest.io/

using System.Diagnostics;
using System.Reflection;

var solutions = typeof(DayAttribute).Assembly.DefinedTypes
    .Select(static t => (type: t, day: t.GetCustomAttribute<DayAttribute>()!))
    .Where(t => t.day is not null)
    .OrderByDescending(static t => t.day.Year)
    .ThenByDescending(t => t.day.Day)
    .ToArray();

#if RELEASE
while (true)
{
#endif

var (solution, day) = ConsoleMenu.Helpers.Menu("Select Day", solutions, static s => s.day.ToString());
var folder = Path.Combine("CodingQuest.App", day.Year.ToString(), day.Day.ToString());
var inputs = Directory.EnumerateFiles(folder, "*.refin", SearchOption.AllDirectories).ToArray();
var input = ConsoleMenu.Helpers.Menu("Select input", inputs, static file => file);
var refout = input.Replace(".refin", ".refout");
#pragma warning disable CS0642 // Possible mistaken empty statement
using (_ = File.OpenWrite(refout)) ;
#pragma warning restore CS0642 // Possible mistaken empty statement
var output = File.ReadAllLines(refout);
var instance = (ISolution)Activator.CreateInstance(solution, File.ReadAllText(input))!;

var sw = new Stopwatch();
Console.WriteLine(day.Title);
for (int i = 0; i < instance.RunCount; i++)
{
    sw.Restart();
    var result = instance.Run(i);
    sw.Stop();
    (var outcome, Console.ForegroundColor)
        = output.Length <= i ? ("?", ConsoleColor.Blue)
        : result == output[i] ? ("✓", ConsoleColor.Green)
        : ("X", ConsoleColor.Red);
    Console.WriteLine($"    [{outcome}] {result} in {sw.Elapsed}");
}
Console.ResetColor();

#if RELEASE
Console.ReadLine();
}
#endif
