using System.Text;

namespace FrameCalculator.Report;

public interface IReporter
{
    public void Report(StringBuilder stringBuilder);
}