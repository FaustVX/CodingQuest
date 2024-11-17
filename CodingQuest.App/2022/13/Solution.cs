using System.Text;
using BigGustave;

namespace CQ_2022_13;

[Day(2022, 13, id:10, "A special painting")]
sealed partial class Solution([Field(Type = typeof(Uri), AssignFormat = """new({0})""")]string input) : ISolution
{
    public int RunCount => 1;

    public string Run(int index)
    {
        var png = CreatePNG(_input);
        return Globals.IsTest ? RunTest(png) : Run1(png);

        static Png CreatePNG(Uri uri)
        {
            using var stream = new HttpClient().GetStreamAsync(uri).Result;
            using var ms = new MemoryStream();
            stream.CopyTo(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return Png.Open(ms);
        }
    }

    public string Run1(Png png)
    {
        var (width, height) = (png.Width, png.Height);
        var datas = (stackalloc byte[width * height / 8]);
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (SetBit(datas, (png.GetPixel(x, y).R & 0b1) == 0b1, y * width + x))
                    goto @return;
            }
        }
@return:
        return Encoding.ASCII.GetString(datas[datas.LastIndexOf((byte)' ')..datas.IndexOf(default(byte))][1..^1]);
    }

    public string RunTest(Png png)
    {
        var (width, height) = (png.Width / 16, png.Height / 16);
        var datas = (stackalloc byte[width * height / 8]);
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (SetBit(datas, (png.GetPixel(x * 16 + 8, y * 16 + 8).R & 0b1) == 0b1, y * width + x))
                    goto @return;
            }
        }
@return:
        return Encoding.ASCII.GetString(datas[..datas.IndexOf(default(byte))]); // image quality is not good enough to produce acurate datas.
    }

    static bool SetBit(Span<byte> datas, bool value, int pos)
    {
        if (value)
            datas[pos / 8] |= (byte)(0b1 << (7 - pos % 8));
        return !value && pos % 8 == 7 && datas[pos / 8] == 0;
    }
}
