using System.Text;
using FrameCalculator.Storage;

namespace FrameCalculator.Report;

public class BoardsCountReport : IReporter
{
    private readonly StorageByName _storage;

    public BoardsCountReport(StorageByName storage)
    {
        _storage = storage;
    }


    public void Report(StringBuilder stringBuilder, string layer)
    {
        stringBuilder.AppendLine($"Boards count in layer \"{layer}\":");
        Build(stringBuilder);
    }

    public void Report(StringBuilder stringBuilder)
    {
        stringBuilder.AppendLine("Total boards count:");
        Build(stringBuilder);
    }

    private void Build(StringBuilder stringBuilder)
    {
        stringBuilder.AppendLine();

        foreach (var (name, countByLength) in _storage)
        {
            stringBuilder.AppendLine("--------------------------------------------------");
            stringBuilder.AppendLine($"-{name}: ");
            stringBuilder.AppendLine();
            foreach (var (lenght, count) in countByLength)
            {
                stringBuilder.AppendLine($"{lenght}mm x {count}");
            }
        }

        stringBuilder.AppendLine("==================================================");
    }
}