using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elmah.CoreCommonBLL
{
    public class ElmahStatusCodeDataStreamService
        : FrameworkCore.Services.DataStreamServiceProviderBase<List<Elmah.DataSourceEntities.ElmahStatusCode>, Elmah.DataSourceEntities.ElmahStatusCode>
    {
        public override void WriteHeaderLineToClosedXmlWorkSheet(ClosedXML.Excel.IXLWorksheet worksheet, int row)
        {
            char cell = 'A';

            string cellKey;
            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = "StatusCode";

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = "Name";

        }

        public override void WriteDataItemToClosedXmlWorkSheet(ClosedXML.Excel.IXLWorksheet worksheet, Elmah.DataSourceEntities.ElmahStatusCode dataItem, int row)
        {
            char cell = 'A';

            string cellKey;
            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = dataItem.StatusCode;

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = dataItem.Name;

        }

    }
}

