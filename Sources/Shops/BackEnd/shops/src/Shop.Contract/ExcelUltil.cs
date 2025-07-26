using ClosedXML.Excel;

namespace Shop.Contract;
public static class ExcelUltil
{
    public static List<Dictionary<string, string>> ReadExcelByKey(Stream excelStream, int startRow = 1)
    {
        var result = new List<Dictionary<string, string>>();

        using var workbook = new XLWorkbook(excelStream);
        var worksheet = workbook.Worksheets.First();
        var headerRow = worksheet.FirstRowUsed();
        var headers = headerRow.Cells().Select(c => c.GetString()).ToList();
        foreach (var dataRow in worksheet.RowsUsed().Skip(startRow)) // Bỏ dòng tiêu đề
        {
            var rowDict = new Dictionary<string, string>();
            var cells = dataRow.Cells().ToList();

            for (int i = 0; i < headers.Count; i++)
            {
                var key = headers[i];
                var value = i < cells.Count ? cells[i].GetString() : "";
                rowDict[key] = value;
            }

            result.Add(rowDict);
        }

        return result;
    }

}
