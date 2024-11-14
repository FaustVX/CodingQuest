// See https://aka.ms/new-console-template for more information
namespace CodingQuest;

public interface ISolution
{
    public abstract int RunCount { get; }
    public abstract string Run(int index);
}
