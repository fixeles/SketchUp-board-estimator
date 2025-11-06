namespace FrameCalculator.Storage;

/// <summary>
/// Count by name
/// </summary>
public class SolidBoardStorage : Dictionary<string, int>
{
    private const int SolidBoardLength = 6000;

    public SolidBoardStorage(StorageByName storageByName)
    {
        foreach (var (name, storage) in storageByName)
        {
            Add(name, CalculateSolidBoardsCount(storage));
        }
    }

    private int CalculateSolidBoardsCount(CountByLength storage)
    {
        if (storage.Count == 0) return 0;

        var processedStorage = HandleBoardOversize(storage);

        // Создаем список отрезков, отсортированный по убыванию
        var allPieces = processedStorage
            .SelectMany(pair => Enumerable.Repeat(pair.Key, pair.Value))
            .ToList();
        

        var shortestLenght = allPieces[^1];

        int boardsCount = 0;

        while (allPieces.Count > 0)
        {
            boardsCount++;
            int remainingLength = SolidBoardLength;

            // Жадный алгоритм: берем самые большие отрезки, которые помещаются
            for (int i = 0; i < allPieces.Count; i++)
            {
                if (remainingLength < shortestLenght)
                    break;

                if (allPieces[i] > remainingLength)
                    continue;
                
                remainingLength -= allPieces[i];
                allPieces.RemoveAt(i);
                i--; // Уменьшаем счетчик т.к. удалили элемент
            }
        }

        return boardsCount;
    }

    private IOrderedEnumerable<KeyValuePair<int, int>> HandleBoardOversize(CountByLength storage)
    {
        var orderedStorage = storage.OrderByDescending(b => b.Key);
        if (orderedStorage.First().Key < SolidBoardLength)
            return orderedStorage;

        var oversizeCuts = new Dictionary<int, int>();
        var toRemove = new List<int>();
        foreach (var (length, count) in orderedStorage)
        {
            if (length <= SolidBoardLength)
                break;

            var solidBoards = length / SolidBoardLength;
            solidBoards *= count;

            if (!oversizeCuts.TryAdd(SolidBoardLength, solidBoards))
                oversizeCuts[SolidBoardLength] += solidBoards;


            var lengthLeft = length % SolidBoardLength;

            if (!oversizeCuts.TryAdd(lengthLeft, count))
                oversizeCuts[lengthLeft] += count;

            toRemove.Add(length);
        }

        if (oversizeCuts.Count == 0)
            return orderedStorage;

        var updatedStorage = orderedStorage.ToDictionary();
        foreach (var lenght in toRemove)
        {
            updatedStorage.Remove(lenght);
        }

        return updatedStorage.Concat(oversizeCuts)
            .GroupBy(pair => pair.Key)
            .ToDictionary(group => group.Key,
                group => group.Sum(pair => pair.Value))
            .OrderByDescending(b => b.Key);
    }
}