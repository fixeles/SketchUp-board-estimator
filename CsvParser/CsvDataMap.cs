using CsvHelper.Configuration;

namespace FrameCalculator.CsvParser;

public class CsvData
{
    public string Name { get; set; }
    public string Layer { get; set; }
    public string Volume { get; set; }
}

public sealed class CsvDataMap : ClassMap<CsvData>
{
    public CsvDataMap()
    {
        Map(m => m.Name).Name("ПУТЬ");
        Map(m => m.Layer).Name("СЛОЙ");
        Map(m => m.Volume).Name("ОБЪЕМ ОБЪЕКТА");
    }
}