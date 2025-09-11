using System.Text;

namespace FrameCalculator.Report;

public class RootReporter : IReporter
{
    private readonly IReporter[] _reporters;

    public RootReporter(params IReporter[] reporters)
    {
        _reporters = reporters;
    }

    public void Report(StringBuilder stringBuilder = null!)
    {
        stringBuilder = new();
        foreach (var reporter in _reporters)
        {
            reporter.Report(stringBuilder);
        }

        Console.WriteLine(stringBuilder.ToString());
    }
}