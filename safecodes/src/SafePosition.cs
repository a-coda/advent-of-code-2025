namespace safecodes;

public class SafePosition
{
    private int Position { get; set; } = 50;
    public int Zeroes { get; set; } = 0;

    public void ProcessCode(Code code)
    {
        switch (code.Direction)
        {
            case Direction.Left:
                Left(code.Steps);
                if (Position == 0)
                    Zeroes++;
                break;
            case Direction.Right:
                Right(code.Steps);
                if (Position == 0)
                    Zeroes++;
                break;
        }
    }

    private void Left(int steps)
    {
        int newPosition = (Position - steps) % 100;
        if (newPosition < 0)
        {
            newPosition += 100;;
        }
        Position = newPosition;
    }

    private void Right(int steps)
    {
        Position = (Position + steps) % 100;
    }
}
