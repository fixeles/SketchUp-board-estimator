using FrameCalculator.CsvParser;

namespace FrameCalculator.Storage;

public class StorageByType
{
    private readonly Dictionary<string, BoardModel> _boardsStorage = new();

    public void AddBoard(CsvData csvData)
    {
        if (!_boardsStorage.ContainsKey(csvData.Name))
        {
            
        }
    }
}