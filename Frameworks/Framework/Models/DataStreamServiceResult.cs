using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Framework.Models
{
    public class DataStreamServiceResult
    {

        public DataStreamServiceResult()
        {
        }

        public DataStreamServiceResult(string fileName, string mimeType, long contentLength, Stream input)
        {
            this.FileName = fileName;
            this.FileExtension = System.IO.Path.GetExtension(fileName);
            this.MIMEType = mimeType;
            this.ContentLength = contentLength;
            this.Result = input;

            if (this.FileExtension.ToLower() == ".xlsx" || this.FileExtension.ToLower() == "xlsx")
            {
                this.DataServiceType = Framework.Models.DataServiceTypes.Excel2010;
            }
            else if (this.FileExtension.ToLower() == ".csv" || this.FileExtension.ToLower() == "csv")
            {
                this.DataServiceType = Framework.Models.DataServiceTypes.Csv;
            }
            else
            {
                this.DataServiceType = Framework.Models.DataServiceTypes.Csv;
            }
        }

        public Framework.Models.DataServiceTypes DataServiceType { get; set; }
        public long ContentLength { get; set; }
        public string MIMEType { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string TempFilePath { get; set; }

        public Stream Result { get; set; }
    }
}

