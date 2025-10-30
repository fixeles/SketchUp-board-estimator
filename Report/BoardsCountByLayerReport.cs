using System.Text;
using FrameCalculator.Storage;

namespace FrameCalculator.Report;

public class BoardsCountByLayerReport : IReporter
{
    private readonly StorageByLayer _storage;

    public BoardsCountByLayerReport(StorageByLayer storage)
    {
        _storage = storage;
    }

    public void Report(StringBuilder stringBuilder)
    {
        foreach (var (layer, storageByName) in _storage)
        {
            new BoardsCountReport(storageByName).Report(stringBuilder, layer);
        }
    }
}