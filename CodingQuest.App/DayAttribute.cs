// See https://aka.ms/new-console-template for more information
namespace CodingQuest;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
sealed partial class DayAttribute([Property]int year, [Property]int day, [Property]int id) : Attribute
{
    public override string ToString()
    => $"{Year}/{Day}";
}
