using FrameCalculator.CsvParser;

namespace FrameCalculator.Storage;

public class BoardsCountStorage
{
    private readonly Dictionary<BoardModel, int> _boardsCount = new();
    
    public void IncrementBoardsCount(string boardName){}
}