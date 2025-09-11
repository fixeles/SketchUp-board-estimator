using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;

namespace FrameCalculator.CsvParser;

public class Reader
{
    public List<CsvData> ParseCsvString()
    {
        const string path = "CSV.txt";
        var encoding = Encoding.UTF8;

        var currentDirectory = Directory.GetCurrentDirectory();
        var filePath = Path.Combine(currentDirectory, path);

        if (!File.Exists(filePath))
            throw new FileNotFoundException($"File {path} not found in: {currentDirectory}");

        using var reader = new StreamReader(filePath, encoding);
        using var csv = new CsvReader(reader,
            new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
                Encoding = encoding,
                HasHeaderRecord = true,
                BadDataFound = null
            });

        csv.Context.RegisterClassMap<CsvDataMap>();

        return csv.GetRecords<CsvData>().ToList();
    }
}