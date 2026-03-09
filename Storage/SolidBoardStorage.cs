namespace FrameCalculator.Storage;

/// <summary>
/// Count by name
/// </summary>
public class SolidBoardStorage : Dictionary<string, int>
{
    public SolidBoardStorage(StorageByName storageByName)
    {
        foreach (var (name, storage) in storageByName)
        {
            Add(name, CalculateSolidBoardsCount(storage));
        }
    }

    private int CalculateSolidBoardsCount(CountByLength storage)
    {
        if (storage.Count == 0) 
            return 0;

        int readyBoards = 0;
        var piecesForCutting = new Dictionary<int, int>();

        foreach (var (length, count) in storage)
        {
            switch (length)
            {
                case Constants.SolidBoardLength:
                    readyBoards += count;
                    break;
                
                case < Constants.SolidBoardLength:
                    AddToDictionary(piecesForCutting, length, count);
                    break;
                
                // length > Constants.SolidBoardLength
                default:
                {
                    // Длинномеры - разбиваем на части
                    int fullPieces = length / Constants.SolidBoardLength;
                    int remainder = length % Constants.SolidBoardLength;

                    readyBoards += fullPieces * count;

                    if (remainder > 0)
                        AddToDictionary(piecesForCutting, remainder, count);
                    break;
                }
            }
        }

        if (piecesForCutting.Count == 0)
            return readyBoards;

        //Оптимизируем раскрой оставшихся отрезков
        var allPieces = piecesForCutting
            .SelectMany(pair => Enumerable.Repeat(pair.Key, pair.Value))
            .OrderByDescending(x => x)
            .ToList();

        var shortestLength = allPieces.Min();
        int boardsForCutting = 0;

        while (allPieces.Count > 0)
        {
            boardsForCutting++;
            int remainingLength = Constants.SolidBoardLength;

            for (int i = 0; i < allPieces.Count; i++)
            {
                if (remainingLength < shortestLength)
                    break;

                if (allPieces[i] > remainingLength)
                    continue;

                remainingLength -= allPieces[i];
                allPieces.RemoveAt(i);
                i--;
            }
        }

        return readyBoards + boardsForCutting;
    }

    private void AddToDictionary(Dictionary<int, int> dict, int key, int value)
    {
        if (!dict.TryAdd(key, value))
            dict[key] += value;
    }
}