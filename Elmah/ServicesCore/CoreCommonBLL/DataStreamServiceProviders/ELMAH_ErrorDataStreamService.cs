using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elmah.CoreCommonBLL
{
    public class ELMAH_ErrorDataStreamService
        : FrameworkCore.Services.DataStreamServiceProviderBase<List<Elmah.DataSourceEntities.ELMAH_Error>, Elmah.DataSourceEntities.ELMAH_Error>
    {
        public override void WriteHeaderLineToClosedXmlWorkSheet(ClosedXML.Excel.IXLWorksheet worksheet, int row)
        {
            char cell = 'A';

            string cellKey;
            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = "ErrorId";

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = "Application";

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = "Host";

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = "Type";

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = "Source";

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = "Message";

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = "User";

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = "StatusCode";

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = "TimeUtc";

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = "Sequence";

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = "AllXml";

        }

        public override void WriteDataItemToClosedXmlWorkSheet(ClosedXML.Excel.IXLWorksheet worksheet, Elmah.DataSourceEntities.ELMAH_Error dataItem, int row)
        {
            char cell = 'A';

            string cellKey;
            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = dataItem.ErrorId;

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = dataItem.Application;

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = dataItem.Host;

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = dataItem.Type;

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = dataItem.Source;

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = dataItem.Message;

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = dataItem.User;

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = dataItem.StatusCode;

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = dataItem.TimeUtc;

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = dataItem.Sequence;

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = dataItem.AllXml;

        }

        public class Default
            : FrameworkCore.Services.DataStreamServiceProviderBase<List<Elmah.DataSourceEntities.ELMAH_Error.Default>, Elmah.DataSourceEntities.ELMAH_Error.Default>
        {
            public override void WriteHeaderLineToClosedXmlWorkSheet(ClosedXML.Excel.IXLWorksheet worksheet, int row)
            {
                char cell = 'A';

                string cellKey;
                cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = "ElmahApplication_Name";

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = "ErrorId";

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = "ElmahHost_Name";

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = "ElmahSource_Name";

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = "ElmahStatusCode_Name";

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = "ElmahType_Name";

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = "ElmahUser_Name";

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = "Application";

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = "Host";

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = "Type";

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = "Source";

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = "Message";

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = "User";

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = "StatusCode";

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = "TimeUtc";

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = "Sequence";

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = "AllXml";

            }

            public override void WriteDataItemToClosedXmlWorkSheet(ClosedXML.Excel.IXLWorksheet worksheet, Elmah.DataSourceEntities.ELMAH_Error.Default dataItem, int row)
            {
                char cell = 'A';

                string cellKey;
                cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = dataItem.ElmahApplication_Name;

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = dataItem.ErrorId;

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = dataItem.ElmahHost_Name;

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = dataItem.ElmahSource_Name;

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = dataItem.ElmahStatusCode_Name;

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = dataItem.ElmahType_Name;

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = dataItem.ElmahUser_Name;

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = dataItem.Application;

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = dataItem.Host;

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = dataItem.Type;

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = dataItem.Source;

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = dataItem.Message;

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = dataItem.User;

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = dataItem.StatusCode;

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = dataItem.TimeUtc;

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = dataItem.Sequence;

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = dataItem.AllXml;

            }
        }

    }
}

