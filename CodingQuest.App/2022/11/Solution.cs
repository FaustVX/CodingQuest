namespace CQ_2022_11;

[Day(2022, 11, id:8, "Message from home")]
sealed partial class Solution([Field(AssignFormat = """{0}.TrimEnd()""")]string input) : ISolution
{
    public string Run()
    {
        var (key, range) = Globals.InputFile[^7] switch
        {
            '1' => ("SECRET", 26..52),
            '2' => ("With great power comes great responsibility", ..),
            '3' => ("With great power comes great responsibility", ..),
            _ => ("Roads? Where We're Going, We Don't Need Roads.", ..),
        };
        return Run1(key, "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789.,;:?! '()".AsSpan(range)).ToString();
    }

    string Run1(ReadOnlySpan<char> key, ReadOnlySpan<char> charSet)
    {
        var output = (stackalloc char[_input.Length]);
        var offsets = (stackalloc int[key.Length]);
        for (int i = 0; i < key.Length; i++)
            offsets[i] = charSet.IndexOf(key[i]);
        for (int i = 0; i < _input.Length; i++)
            if (!charSet.Contains(_input[i]))
                output[i] = _input[i];
            else
                output[i] = charSet[(charSet.IndexOf(_input[i]) - offsets[i % offsets.Length] + charSet.Length - 1) % charSet.Length];
        return output.ToString();
    }
}
