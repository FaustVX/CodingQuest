namespace CodingQuest;

public interface ISolution
{
    public abstract int RunCount { get; }
    public abstract string Run(int index);
}
