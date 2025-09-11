using FrameCalculator.CsvParser;
using FrameCalculator.Storage;

namespace FrameCalculator;

public static class ProgramRoot
{
    public static void Start()
    {
        var parsedCsv = new Reader().ParseCsvString();
        var storage = new MainStorage(parsedCsv);
    }
}