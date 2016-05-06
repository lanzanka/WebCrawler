using Core.DTOs;
using ServiceStack;
using ServiceStack.Text;
using System.IO;
using System.Text;

namespace Core.Crawler
{
    public class CsvOutput : IResultOutput
    {
        public CsvOutput()
        {
            SetCsvConfig();
        }

        private static void SetCsvConfig()
        {
            CsvConfig.ItemSeperatorString = ";";
            CsvConfig<PageData>.OmitHeaders = true;
        }

        public void SavePageData(PageData pageData)
        {
            var buffer = Encoding.UTF8.GetBytes(pageData.ToCsv());
            using (var fileStream = new FileStream("result.csv", FileMode.Append, FileAccess.Write, FileShare.None, 4096, true))
            {
                fileStream.Write(buffer, 0, buffer.Length);
            }
        }
    }
}