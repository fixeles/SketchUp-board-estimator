using System.Globalization;

namespace FrameCalculator.CsvParser;

public static class ModelConvertor
{
    public static bool TryConvert(CsvData parsedCsv, out BoardModel board)
    {
        var hasSection = TryCalculateSection(parsedCsv, out var x, out var y);
        if (!hasSection)
        {
            board = null;
            return false;
        }

        var length = CalculateLenght(parsedCsv, x, y);
        board = new BoardModel(parsedCsv.Name, x, y, length, parsedCsv.Layer);
        return true;
    }

    private static bool TryCalculateSection(CsvData parsedCsv, out int x, out int y)
    {
        char[] separators = ['X', 'x', 'Х', 'х', '*'];
        foreach (var c in separators)
        {
            var split = parsedCsv.Name.Split(c);
            if (split.Length != 2)
                continue;

            x = int.Parse(split[0]);
            y = int.Parse(split[1]);
            return true;
        }

        x = 0;
        y = 0;
        return false;
    }

    private static int CalculateLenght(CsvData parsedCsv, int x, int y)
    {
        const double inchesToMillimetersVolumeModifier = 16387.064;
        double volume = double.Parse(parsedCsv.Volume, CultureInfo.InvariantCulture);
        volume *= inchesToMillimetersVolumeModifier;
        var length = (int)volume / x / y;
        return length;
    }
}