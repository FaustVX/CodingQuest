// https://codingquest.io/

using System.Diagnostics;
using System.Reflection;
using TextCopy;

var solutions = typeof(DayAttribute).Assembly.DefinedTypes
    .Select(static t => (type: t, day: t.GetCustomAttribute<DayAttribute>()!))
    .Where(static t => t.day is not null)
    .OrderByDescending(static t => t.day.Year)
    .ThenByDescending(static t => t.day.Day)
    .ToArray();

#if RELEASE
while (true)
{
#endif

var (solution, day) = ConsoleMenu.Helpers.Menu("Select Day", solutions, static s => s.day.ToString());
var folder = Path.Combine("CodingQuest.App", day.Year.ToString(), day.Day.ToString());
var inputs = Directory.EnumerateFiles(folder, "*.refin", SearchOption.AllDirectories).ToArray();

Globals.InputFile = ConsoleMenu.Helpers.Menu("Select input", inputs, static file => file);
Globals.IsTest = new FileInfo(Globals.InputFile).Directory!.Name == "test";

var instance = (ISolution)Activator.CreateInstance(solution, File.ReadAllText(Globals.InputFile))!;

var refout = Globals.InputFile.Replace(".refin", ".refout");
#pragma warning disable CS0642 // Possible mistaken empty statement
using (_ = File.OpenWrite(refout)) ;
#pragma warning restore CS0642 // Possible mistaken empty statement
var output = File.ReadAllLines(refout);

var sw = new Stopwatch();
Console.WriteLine(day.Title);
for (int i = 0; i < instance.RunCount; i++)
{
    sw.Restart();
    var result = instance.Run(i);
    sw.Stop();

    ClipboardService.SetText(result);

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
