using FrameCalculator.CsvParser;

namespace FrameCalculator.Storage;

public class StorageByLayer : Dictionary<string, StorageByName>
{
    public StorageByLayer(UnsortedStorage unsortedStorage)
    {
        var boardsByLayer = new Dictionary<string, HashSet<BoardModel>>();

        foreach (var board in unsortedStorage)
        {
            if (!boardsByLayer.ContainsKey(board.Layer))
                boardsByLayer.Add(board.Layer, []);

            boardsByLayer[board.Layer].Add(board);
        }

        foreach (var (layer, boards) in boardsByLayer)
        {
            Add(layer, new StorageByName(boards));
        }
    }
}