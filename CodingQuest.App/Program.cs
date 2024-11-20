// https://codingquest.io/

using System.Collections.Frozen;
using System.Diagnostics;
using System.Reflection;
using TextCopy;

var solutions = typeof(DayAttribute).Assembly.DefinedTypes
    .Select(static t => (type: t, day: t.GetCustomAttribute<DayAttribute>()!))
    .Where(static t => t.day is not null)
    .OrderByDescending(static t => t.day.Day)
    .GroupBy(static t => t.day.Year)
    .ToFrozenDictionary(static t => t.Key, static t => t.ToArray());

#if RELEASE
while (true)
{
#endif

var year = ConsoleMenu.Helpers.Menu("Select Year", solutions.Keys, static s => s.ToString());
var (solution, day) = ConsoleMenu.Helpers.Menu("Select Day", solutions[year], static s => s.day.ToString());
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

Console.WriteLine(day.Title);
var sw = Stopwatch.StartNew();
var result = instance.Run();
sw.Stop();

ClipboardService.SetText(result);
if (!Globals.IsTest && output.Length == 0)
    File.WriteAllText(refout, result + Environment.NewLine);

(var outcome, Console.ForegroundColor)
    = output.Length <= 0 ? ("?", ConsoleColor.Blue)
    : result == output[0] ? ("✓", ConsoleColor.Green)
    : ("X", ConsoleColor.Red);
Console.WriteLine($"    [{outcome}] {result} in {sw.Elapsed}");
Console.ResetColor();

#if RELEASE
Console.ReadLine();
}
#endif
