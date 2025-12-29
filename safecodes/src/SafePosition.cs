using System.Diagnostics;

namespace safecodes;

public class SafePosition
{
    private int Position { get; set; } = 50;
    public int Zeroes { get; set; } = 0;

    public void ProcessCode(Strategy strategy, Code code)
    {
        switch (code.Direction)
        {
            case Direction.Left:
                Left(strategy, code.Steps);
                break;
            case Direction.Right:
                Right(strategy, code.Steps);
                break;
        }
    }

    private void Left(Strategy strategy, int steps)
    {
        int newPosition = (Position - steps) % 100;
        if (newPosition < 0)
        {
            if (strategy == Strategy.AnyClick)
            {
                int zeroes = (Math.Abs(newPosition) / 100) + 1;
                Console.WriteLine($"Left Move: Pos {Position} - Steps {steps} => Zeroes {zeroes}");
                Zeroes += zeroes;
            }
            newPosition += 100;
        }
        if (newPosition == 0 && strategy == Strategy.EndOfTurn)
        {
            Console.WriteLine($"Left Move to Zero: Pos {Position} - Steps {steps}");
            Zeroes++;
        }
        Position = newPosition;
    }

    private void Right(Strategy strategy, int steps)
    {
        if (strategy == Strategy.AnyClick)
        {
            int zeroes = (Position + steps) / 100;
            Console.WriteLine($"Right Move: Pos {Position} + Steps {steps} => Zeroes {zeroes}");
            Zeroes += zeroes;
        }
        int newPosition = (Position + steps) % 100;
        if (newPosition == 0 && strategy == Strategy.EndOfTurn)
        {
            Console.WriteLine($"Right Move to Zero: Pos {Position} + Steps {steps}");
            Zeroes++;
        }
        Position = newPosition;
    }
}
