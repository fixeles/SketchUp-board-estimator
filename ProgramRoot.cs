using FrameCalculator.CsvParser;
using FrameCalculator.Report;
using FrameCalculator.Storage;

namespace FrameCalculator;

public static class ProgramRoot
{
    public static void Start()
    {
        var parsedCsv = new Reader().ParseCsvString();
        var storage = new UnsortedStorage(parsedCsv);

        ConfigureReport(storage);
    }

    private static void ConfigureReport(UnsortedStorage storage)
    {
        var storageByName = new StorageByName(storage);
        var storageByLayer = new StorageByLayer(storage);
        var solidBoards = new SolidBoardStorage(storageByName);
        
        IReporter[] reporters =
        [
            //add reporters here
            new BoardsCountReport(storageByName),
            // new BoardsCountByLayerReport(storageByLayer),
        ];
        var report = new RootReporter(reporters);
        report.Report();
    }
}