namespace FrameCalculator.CsvParser;

public class BoardModel
{
    public readonly string Name;
    public readonly int X;
    public readonly int Y;
    public readonly int Length;

    public BoardModel(
        string name,
        int x,
        int y,
        int length)
    {
        Name = name;
        X = x;
        Y = y;
        Length = RoundToNearestMultiple(length, 5);
    }

    private int RoundToNearestMultiple(int number, int target)
    {
        return (int)Math.Round((double)number / target) * target;
    }
}