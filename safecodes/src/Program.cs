namespace safecodes;

public class Program
{
    public static void Main(string[] args)
    {
        SafePosition safePosition = new SafePosition();
        using (var stream = new FileStream(@"C:\Users\jason\source\repos\advent-of-code-2025\safecodes\src\puzzleinput.txt", FileMode.Open, FileAccess.Read))
        {
            ProcessCodes(Strategy.EndOfTurn, safePosition, stream);
        }

        Console.WriteLine($"Final Position: {safePosition.Zeroes}");
    }

    public static IEnumerable<string> ReadCodesFromStream(Stream stream)
    {
        using (var reader = new StreamReader(stream))
        {
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                yield return line;
            }
        }
    }

    public static IEnumerable<Code> ParseCodes(IEnumerable<string> codes)
    {
        return codes.Select(code => ParseCode(code));
    }

    public static void ProcessCodes(Strategy strategy, SafePosition safePosition, Stream stream)
    {
        foreach (var code in ParseCodes(ReadCodesFromStream(stream)))
        {
            safePosition.ProcessCode(strategy, code);
        }
    }

    private static Code ParseCode(string code)
    {
        var direction = code[0] switch
        {
            'L' => Direction.Left,
            'R' => Direction.Right,
            _ => throw new ArgumentException("Invalid direction")
        };

        int steps = int.Parse(code.Substring(1));
        return new Code(direction, steps);
    }
}