namespace CodingQuest;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
sealed partial class DayAttribute([Property]int year, [Property]int day, [Property]int id, [Property]string title) : Attribute
{
    public override string ToString()
    => $"{Title} - {Year}/{Day}";
}
