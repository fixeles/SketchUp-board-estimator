using FrameCalculator.CsvParser;

namespace FrameCalculator.Storage;

public class StorageByName : Dictionary<string, CountByLength>
{
    public StorageByName(IEnumerable<BoardModel> unsortedStorage)
    {
        foreach (var board in unsortedStorage)
        {
            if (!ContainsKey(board.Name))
                Add(board.Name, new CountByLength());

            var countByLength = this[board.Name];
            if (!countByLength.ContainsKey(board.Length))
                countByLength.Add(board.Length, 0);

            countByLength[board.Length]++;
        }
    }
}