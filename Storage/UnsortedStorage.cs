using System.Collections;
using FrameCalculator.CsvParser;

namespace FrameCalculator.Storage;

public class UnsortedStorage : IEnumerable<BoardModel>
{
    private readonly BoardModel[] _boards;

    public UnsortedStorage(IEnumerable<CsvData> parsedCsv)
    {
        HashSet<BoardModel> boards = [];
        foreach (var csvData in parsedCsv)
        {
            if (!ModelConvertor.TryConvert(csvData, out var board))
                continue;

            boards.Add(board);
        }

        _boards = boards.ToArray();
    }

    public IEnumerator<BoardModel> GetEnumerator() => _boards.AsEnumerable().GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}