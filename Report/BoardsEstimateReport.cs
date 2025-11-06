using System.Text;
using FrameCalculator.Storage;

namespace FrameCalculator.Report;

public class BoardsEstimateReport : IReporter
{
    private readonly SolidBoardStorage _storage;

    public BoardsEstimateReport(SolidBoardStorage storage)
    {
        _storage = storage;
    }

    public void Report(StringBuilder stringBuilder)
    {
        stringBuilder.AppendLine("Total boards estimate:");
        Build(stringBuilder);
    }
    
    private void Build(StringBuilder stringBuilder)
    {
        stringBuilder.AppendLine();

        foreach (var (name, count) in _storage)
        {
            stringBuilder.AppendLine($"-{name} : {count} ");
        }

        stringBuilder.AppendLine("==================================================");
    }
}