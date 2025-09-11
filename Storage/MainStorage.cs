using System.Globalization;
using FrameCalculator.CsvParser;

namespace FrameCalculator.Storage;

public class MainStorage
{
    private readonly Dictionary<string, HashSet<BoardModel>> _boardsByLayer = new();

    public MainStorage(IEnumerable<CsvData> parsedCsv)
    {
        foreach (var csvData in parsedCsv)
        {
            if (!TryConvert(csvData, out var board))
                continue;

            if (!_boardsByLayer.ContainsKey(csvData.Layer))
                _boardsByLayer.Add(csvData.Layer, new HashSet<BoardModel>());

            _boardsByLayer[csvData.Layer].Add(board);
        }
    }

    private bool TryConvert(CsvData parsedCsv, out BoardModel board)
    {
        var hasSection = TryCalculateSection(parsedCsv, out var x, out var y);
        if (!hasSection)
        {
            board = null;
            return false;
        }

        var length = CalculateLenght(parsedCsv, x, y);
        board = new BoardModel(parsedCsv.Name, x, y, length);
        return true;
    }

    private bool TryCalculateSection(CsvData parsedCsv, out int x, out int y)
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

    private int CalculateLenght(CsvData parsedCsv, int x, int y)
    {
        const double inchesToMillimetersVolumeModifier = 16387.064;
        double volume = double.Parse(parsedCsv.Volume, CultureInfo.InvariantCulture);
        volume *= inchesToMillimetersVolumeModifier;
        var length = (int)volume / x / y;
        return length;
    }
}