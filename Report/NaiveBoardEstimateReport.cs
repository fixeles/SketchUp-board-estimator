using System.Text;
using FrameCalculator.Storage;

namespace FrameCalculator.Report;

public class NaiveBoardEstimateReport : IReporter
{
    private readonly StorageByName _storage;

    public NaiveBoardEstimateReport(StorageByName storage)
    {
        _storage = storage;
    }

    public void Report(StringBuilder stringBuilder)
    {
        stringBuilder.AppendLine("Примитивный подсчет (длины всех досок / 6м) + 6м)");
        stringBuilder.AppendLine("Naive boards estimate:");
        Build(stringBuilder);
    }

    private void Build(StringBuilder stringBuilder)
    {
        stringBuilder.AppendLine();

        foreach (var (name, countByLength) in _storage)
        {
            var allPieces = countByLength
                .SelectMany(pair => Enumerable.Repeat(pair.Key, pair.Value))
                .ToList();

            var boardsCount = allPieces.Sum() / Constants.SolidBoardLength + 1;
            stringBuilder.AppendLine($"-{name} : {boardsCount}");
        }

        stringBuilder.AppendLine("==================================================");
    }
}