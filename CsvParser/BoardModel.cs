namespace FrameCalculator.CsvParser;

public class BoardModel
{
    public readonly string Name;
    public readonly string Layer;
    public readonly int X;
    public readonly int Y;
    public readonly int Length;

    public BoardModel(
        string name,
        int x,
        int y,
        int length,
        string layer)
    {
        Name = name;
        X = x;
        Y = y;
        Layer = layer;
        // Length = RoundToNearestMultiple(length, 5);
        Length = length;
    }

    private int RoundToNearestMultiple(int number, int target)
    {
        return (int)Math.Round((double)number / target) * target;
    }
}